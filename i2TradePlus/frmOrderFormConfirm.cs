using i2TradePlus.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class frmOrderFormConfirm : Form
	{
		public enum OpenStyle
		{
			ShowBox,
			ConfirmCancel,
			ConfirmSendNew,
			Error,
			WaitingForm
		}
		private delegate void ShowTextCallBack(string message);
		private IContainer components = null;
		private Button btnCancel;
		private Button btnOk;
		private Button btnNo;
		private Label lbPincode;
		private TextBox tbPincode;
		private CheckBox chbSavePincode;
		private GroupBox gbSavePingDisclaimer;
		private Button btnSavePinDisAgree;
		private Button btnSavePinAgree;
		private Label lbSavePin;
		private Panel panel1;
		private Label lbStockTH;
		private Label lbStockTHlabel;
		private Label lbOrderString;
		private Label lbOSS;
		private Label lbOSSLabel;
		private DialogResult result = DialogResult.None;
		private frmOrderFormConfirm.OpenStyle openFormStyle = frmOrderFormConfirm.OpenStyle.ShowBox;
		private string _ossMessage = string.Empty;
		private string _stockThreshold = string.Empty;
		private string _orderParam = string.Empty;
		private string _message = string.Empty;
		private bool _isShowPin = false;
		private Timer tmCloseForm = null;
		public DialogResult Result
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.result;
			}
		}
		public frmOrderFormConfirm.OpenStyle OpenFormStyle
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.openFormStyle;
			}
		}
		public string OssMessage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._ossMessage;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._ossMessage = value;
			}
		}
		public string StockThreshold
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._stockThreshold;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._stockThreshold = value;
			}
		}
		public string OrderParam
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._orderParam;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._orderParam = value;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmOrderFormConfirm));
			this.btnCancel = new Button();
			this.btnOk = new Button();
			this.btnNo = new Button();
			this.lbPincode = new Label();
			this.tbPincode = new TextBox();
			this.chbSavePincode = new CheckBox();
			this.gbSavePingDisclaimer = new GroupBox();
			this.btnSavePinDisAgree = new Button();
			this.btnSavePinAgree = new Button();
			this.lbSavePin = new Label();
			this.panel1 = new Panel();
			this.lbOSS = new Label();
			this.lbOSSLabel = new Label();
			this.lbStockTH = new Label();
			this.lbStockTHlabel = new Label();
			this.lbOrderString = new Label();
			this.gbSavePingDisclaimer.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.btnCancel.BackColor = Color.WhiteSmoke;
			this.btnCancel.FlatAppearance.BorderColor = Color.Gray;
			this.btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnCancel.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnCancel.FlatStyle = FlatStyle.Flat;
			this.btnCancel.Location = new Point(272, 197);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(87, 29);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnCancel.Leave += new EventHandler(this.Button_Leave);
			this.btnCancel.Enter += new EventHandler(this.Button_Enter);
			this.btnOk.BackColor = Color.WhiteSmoke;
			this.btnOk.FlatAppearance.BorderColor = Color.Gray;
			this.btnOk.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnOk.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnOk.FlatStyle = FlatStyle.Flat;
			this.btnOk.Location = new Point(51, 198);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new Size(87, 29);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Yes";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.btnOk.Leave += new EventHandler(this.Button_Leave);
			this.btnOk.Enter += new EventHandler(this.Button_Enter);
			this.btnNo.BackColor = Color.WhiteSmoke;
			this.btnNo.FlatAppearance.BorderColor = Color.Gray;
			this.btnNo.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnNo.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnNo.FlatStyle = FlatStyle.Flat;
			this.btnNo.Location = new Point(160, 197);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new Size(87, 29);
			this.btnNo.TabIndex = 1;
			this.btnNo.Text = "No";
			this.btnNo.UseVisualStyleBackColor = false;
			this.btnNo.Click += new EventHandler(this.btnNo_Click);
			this.btnNo.Leave += new EventHandler(this.Button_Leave);
			this.btnNo.Enter += new EventHandler(this.Button_Enter);
			this.lbPincode.AutoSize = true;
			this.lbPincode.Location = new Point(10, 203);
			this.lbPincode.Name = "lbPincode";
			this.lbPincode.Size = new Size(32, 21);
			this.lbPincode.TabIndex = 2;
			this.lbPincode.Text = "Pin";
			this.tbPincode.Location = new Point(39, 200);
			this.tbPincode.MaxLength = 10;
			this.tbPincode.Name = "tbPincode";
			this.tbPincode.PasswordChar = '*';
			this.tbPincode.Size = new Size(72, 27);
			this.tbPincode.TabIndex = 0;
			this.tbPincode.KeyDown += new KeyEventHandler(this.tbPincode_KeyDown);
			this.tbPincode.Leave += new EventHandler(this.Button_Leave);
			this.tbPincode.Enter += new EventHandler(this.Button_Enter);
			this.chbSavePincode.AutoSize = true;
			this.chbSavePincode.Location = new Point(116, 202);
			this.chbSavePincode.Name = "chbSavePincode";
			this.chbSavePincode.Size = new Size(67, 25);
			this.chbSavePincode.TabIndex = 0;
			this.chbSavePincode.TabStop = false;
			this.chbSavePincode.Text = "Save";
			this.chbSavePincode.UseVisualStyleBackColor = true;
			this.chbSavePincode.Click += new EventHandler(this.chbSavePincode_Click);
			this.gbSavePingDisclaimer.BackColor = Color.FromArgb(224, 224, 224);
			this.gbSavePingDisclaimer.Controls.Add(this.btnSavePinDisAgree);
			this.gbSavePingDisclaimer.Controls.Add(this.btnSavePinAgree);
			this.gbSavePingDisclaimer.Controls.Add(this.lbSavePin);
			this.gbSavePingDisclaimer.Dock = DockStyle.Fill;
			this.gbSavePingDisclaimer.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.gbSavePingDisclaimer.Location = new Point(0, 0);
			this.gbSavePingDisclaimer.Name = "gbSavePingDisclaimer";
			this.gbSavePingDisclaimer.Size = new Size(479, 231);
			this.gbSavePingDisclaimer.TabIndex = 7;
			this.gbSavePingDisclaimer.TabStop = false;
			this.gbSavePingDisclaimer.Text = "Disclaimer";
			this.gbSavePingDisclaimer.Visible = false;
			this.btnSavePinDisAgree.Location = new Point(200, 72);
			this.btnSavePinDisAgree.Name = "btnSavePinDisAgree";
			this.btnSavePinDisAgree.Size = new Size(96, 23);
			this.btnSavePinDisAgree.TabIndex = 2;
			this.btnSavePinDisAgree.Text = "I DISAGREE";
			this.btnSavePinDisAgree.UseVisualStyleBackColor = true;
			this.btnSavePinDisAgree.Click += new EventHandler(this.btnSavePinDisAgree_Click);
			this.btnSavePinAgree.Location = new Point(114, 72);
			this.btnSavePinAgree.Name = "btnSavePinAgree";
			this.btnSavePinAgree.Size = new Size(72, 23);
			this.btnSavePinAgree.TabIndex = 1;
			this.btnSavePinAgree.Text = "I AGREE";
			this.btnSavePinAgree.UseVisualStyleBackColor = true;
			this.btnSavePinAgree.Click += new EventHandler(this.btnSavePinAgree_Click);
			this.lbSavePin.AutoSize = true;
			this.lbSavePin.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbSavePin.Location = new Point(6, -19);
			this.lbSavePin.Name = "lbSavePin";
			this.lbSavePin.Size = new Size(508, 108);
			this.lbSavePin.TabIndex = 0;
			this.lbSavePin.Text = componentResourceManager.GetString("lbSavePin.Text");
			this.panel1.BackColor = Color.FromArgb(10, 10, 10);
			this.panel1.Controls.Add(this.lbOSS);
			this.panel1.Controls.Add(this.lbOSSLabel);
			this.panel1.Controls.Add(this.lbStockTH);
			this.panel1.Controls.Add(this.lbStockTHlabel);
			this.panel1.Controls.Add(this.lbOrderString);
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(478, 190);
			this.panel1.TabIndex = 8;
			this.lbOSS.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.lbOSS.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbOSS.ForeColor = Color.FromArgb(255, 128, 0);
			this.lbOSS.Location = new Point(24, 159);
			this.lbOSS.Name = "lbOSS";
			this.lbOSS.Size = new Size(443, 23);
			this.lbOSS.TabIndex = 6;
			this.lbOSS.Text = "Warning String";
			this.lbOSSLabel.AutoSize = true;
			this.lbOSSLabel.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbOSSLabel.ForeColor = Color.LightGray;
			this.lbOSSLabel.Location = new Point(8, 141);
			this.lbOSSLabel.Name = "lbOSSLabel";
			this.lbOSSLabel.Size = new Size(140, 21);
			this.lbOSSLabel.TabIndex = 5;
			this.lbOSSLabel.Text = "Order Screening :";
			this.lbStockTH.AutoSize = true;
			this.lbStockTH.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbStockTH.ForeColor = Color.Red;
			this.lbStockTH.Location = new Point(24, 118);
			this.lbStockTH.Name = "lbStockTH";
			this.lbStockTH.Size = new Size(137, 23);
			this.lbStockTH.TabIndex = 4;
			this.lbStockTH.Text = "Warning String";
			this.lbStockTHlabel.AutoSize = true;
			this.lbStockTHlabel.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbStockTHlabel.ForeColor = Color.LightGray;
			this.lbStockTHlabel.Location = new Point(8, 97);
			this.lbStockTHlabel.Name = "lbStockTHlabel";
			this.lbStockTHlabel.Size = new Size(110, 21);
			this.lbStockTHlabel.TabIndex = 3;
			this.lbStockTHlabel.Text = "Risk Control :";
			this.lbOrderString.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.lbOrderString.AutoSize = true;
			this.lbOrderString.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbOrderString.ForeColor = Color.Yellow;
			this.lbOrderString.Location = new Point(8, 5);
			this.lbOrderString.Name = "lbOrderString";
			this.lbOrderString.Size = new Size(114, 23);
			this.lbOrderString.TabIndex = 0;
			this.lbOrderString.Text = "Order String";
			base.AutoScaleDimensions = new SizeF(9f, 19f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(479, 231);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.chbSavePincode);
			base.Controls.Add(this.tbPincode);
			base.Controls.Add(this.lbPincode);
			base.Controls.Add(this.btnNo);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.gbSavePingDisclaimer);
			this.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.KeyPreview = true;
			base.Name = "frmOrderFormConfirm";
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "Order Confirmation";
			base.Load += new EventHandler(this.frmOrderFormConfirm_Load);
			base.Shown += new EventHandler(this.frmOrderFormConfirm_Shown);
			base.KeyDown += new KeyEventHandler(this.frmOrderFormConfirm_KeyDown);
			this.gbSavePingDisclaimer.ResumeLayout(false);
			this.gbSavePingDisclaimer.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmOrderFormConfirm(string message, frmOrderFormConfirm.OpenStyle openStyle)
		{
			this.InitializeComponent();
			this.openFormStyle = openStyle;
			this._message = message;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmOrderFormConfirm(string message, string caption, frmOrderFormConfirm.OpenStyle openStyle)
		{
			this.InitializeComponent();
			this.Text = caption;
			this.openFormStyle = openStyle;
			this._message = message;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmOrderFormConfirm_Load(object sender, EventArgs e)
		{
			this.Init();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Init()
		{
			if (this.openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmSendNew)
			{
				this.lbOrderString.Text = this._orderParam;
				if (this._orderParam.ToUpper().IndexOf("BUY") > -1)
				{
					this.lbOrderString.ForeColor = Color.Lime;
				}
				else
				{
					if (this._orderParam.ToUpper().IndexOf("SELL") > -1)
					{
						this.lbOrderString.ForeColor = Color.Red;
					}
					else
					{
						if (this._orderParam.ToUpper().IndexOf("SHORT") > -1)
						{
							this.lbOrderString.ForeColor = Color.Magenta;
						}
						else
						{
							if (this._orderParam.ToUpper().IndexOf("COVER") > -1)
							{
								this.lbOrderString.ForeColor = Color.Cyan;
							}
							else
							{
								this.lbOrderString.ForeColor = Color.Yellow;
							}
						}
					}
				}
				if (this._stockThreshold != string.Empty || this._ossMessage != string.Empty)
				{
					this.lbStockTH.Text = this._stockThreshold;
					this.lbOSS.Text = this._ossMessage;
					this.lbStockTH.Visible = true;
					this.lbOSS.Visible = true;
					this.lbOSSLabel.Visible = true;
				}
				else
				{
					this.lbStockTHlabel.Text = this._message;
					this.lbStockTH.Visible = false;
					this.lbOSS.Visible = false;
					this.lbOSSLabel.Visible = false;
				}
			}
			else
			{
				this.ShowText(this._message);
				if (this.openFormStyle == frmOrderFormConfirm.OpenStyle.Error)
				{
					this.lbOrderString.ForeColor = Color.Red;
				}
			}
			this.btnCancel.Visible = false;
			this.btnNo.Visible = false;
			this.btnOk.Visible = false;
			this.SetPinCodeVisible(false);
			this._isShowPin = false;
			switch (this.openFormStyle)
			{
			case frmOrderFormConfirm.OpenStyle.ShowBox:
			case frmOrderFormConfirm.OpenStyle.Error:
				this.btnOk.Visible = true;
				this.btnOk.Text = "Ok";
				this.btnOk.Left = (base.Width - this.btnOk.Width) / 2;
				break;
			case frmOrderFormConfirm.OpenStyle.ConfirmCancel:
			case frmOrderFormConfirm.OpenStyle.ConfirmSendNew:
				this._isShowPin = (ApplicationInfo.UserLoginMode == "C");
				this.SetUpControlOkCancel();
				break;
			}
			this.lbSavePin.Text = string.Concat(new string[]
			{
				"   \"Save PIN\" function allows you to save your PIN for an interval. During this ",
				"\r\ninterval, re-enter PIN is not required for any transaction performed in this",
				"\r\ntrading program only.",
				"\r\n   Therefore, you understand and accept the risk of using this \"Save PIN\"",
				"\r\nfunction, i2Trade will take no responsibility on any loss or damage from any",
				"\r\nerror occurred."
			});
			if (ApplicationInfo.IsScreen125)
			{
				this.lbOrderString.Font = new Font("Tahoma", 8.75f);
				this.lbStockTHlabel.Font = new Font("Tahoma", 8.75f);
				this.lbStockTH.Font = new Font("Tahoma", 8.75f);
				this.lbOSSLabel.Font = new Font("Tahoma", 8.75f);
				this.lbOSS.Font = new Font("Tahoma", 8.75f);
				this.lbSavePin.Font = new Font("Microsoft Sans Serif", 8.75f);
				this.btnOk.Font = new Font("Tahoma", 8.75f);
				this.btnNo.Font = new Font("Tahoma", 8.75f);
				this.btnCancel.Font = new Font("Tahoma", 8.75f);
				this.lbPincode.Font = new Font("Tahoma", 8.75f);
				this.chbSavePincode.Font = new Font("Tahoma", 8.75f);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetPinCodeVisible(bool isVisible)
		{
			this.lbPincode.Visible = isVisible;
			this.tbPincode.Visible = isVisible;
			this.chbSavePincode.Visible = isVisible;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetUpControlOkCancel()
		{
			try
			{
				this.btnOk.Visible = true;
				this.btnNo.Visible = true;
				this.btnOk.Text = "Ok";
				this.btnNo.Text = "Cancel";
				if (this._isShowPin)
				{
					this.SetPinCodeVisible(true);
					this.btnOk.Left = this.chbSavePincode.Left + this.chbSavePincode.Width + 10;
					this.btnNo.Left = this.btnOk.Left + this.btnOk.Width + 10;
				}
				else
				{
					this.btnOk.Left = base.Width / 3 - this.btnOk.Width / 2;
					this.btnNo.Left = base.Width * 2 / 3 - this.btnNo.Width / 2;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetUpControlOkCancel", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetUpControlYesNo()
		{
			try
			{
				this.btnOk.Visible = true;
				this.btnCancel.Visible = true;
				this.btnNo.Visible = true;
				this.btnOk.Text = "Yes";
				this.btnNo.Text = "No";
				this.btnOk.Left = base.Width / 4 - this.btnOk.Width / 2;
				this.btnNo.Left = base.Width * 2 / 4 - this.btnOk.Width / 2;
				this.btnCancel.Left = base.Width * 3 / 4 - this.btnOk.Width / 2;
			}
			catch (Exception ex)
			{
				this.ShowError("SetUpControlYesNo", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowText(string message)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmOrderFormConfirm.ShowTextCallBack(this.ShowText), new object[]
				{
					message
				});
			}
			else
			{
				this.lbStockTHlabel.Text = string.Empty;
				this.lbStockTH.Visible = false;
				this.lbOSS.Visible = false;
				this.lbOSSLabel.Visible = false;
				this.lbOrderString.MaximumSize = new Size(this.panel1.Width - this.lbOrderString.Left * 2, 100);
				this.lbOrderString.Text = message;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.result == DialogResult.None)
				{
					if (this._isShowPin)
					{
						if (ApplicationInfo.AccInfo.UserInternetInBroker == "Y")
						{
							this.ShowText("Internal user not authenrize!");
							return;
						}
						if (ApplicationInfo.UserPincodeWrongCount >= ApplicationInfo.UserMaxRetryPincode)
						{
							this.ShowText("*** Pincode Locked. ***\nPlease logout and login again!");
							this.btnOk.Focus();
							return;
						}
						string empty = string.Empty;
						if (!ApplicationInfo.VerifyPincode(this.tbPincode.Text.Trim(), ref empty))
						{
							this.ShowText(empty);
							this.tbPincode.Focus();
							this.tbPincode.SelectAll();
							return;
						}
					}
					this.result = DialogResult.OK;
					this.ShowText("Waiting...");
					this.CloseMe();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnOk_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void CloseMe()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.CloseMe));
			}
			else
			{
				try
				{
					base.Enabled = false;
					if (!Settings.Default.BSBoxSavePincode)
					{
						ApplicationInfo.UserPincodeLastEntry = string.Empty;
					}
					if (this.tmCloseForm == null)
					{
						this.tmCloseForm = new Timer();
						this.tmCloseForm.Interval = 100;
						this.tmCloseForm.Tick += new EventHandler(this.tmCloseForm_Tick);
					}
					this.tmCloseForm.Stop();
					this.tmCloseForm.Start();
				}
				catch (Exception ex)
				{
					this.ShowError("CloseMe", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmCloseForm_Tick(object sender, EventArgs e)
		{
			this.tmCloseForm.Stop();
			base.Close();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnNo_Click(object sender, EventArgs e)
		{
			if (this.result == DialogResult.None)
			{
				this.result = DialogResult.No;
				this.CloseMe();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (this.result == DialogResult.None)
			{
				this.result = DialogResult.Cancel;
				this.CloseMe();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmOrderFormConfirm_Shown(object sender, EventArgs e)
		{
			if (this._isShowPin)
			{
				if (ApplicationInfo.CheckPinTimeout())
				{
					this.tbPincode.Text = string.Empty;
				}
				else
				{
					this.chbSavePincode.Checked = Settings.Default.BSBoxSavePincode;
					this.tbPincode.Text = ApplicationInfo.UserPincodeLastEntry;
				}
			}
			if (this.tbPincode.Visible && this.tbPincode.Text == string.Empty)
			{
				this.tbPincode.Focus();
			}
			else
			{
				if (this.btnCancel.Visible)
				{
					this.btnCancel.Focus();
				}
				else
				{
					if (this.btnOk.Visible)
					{
						this.btnOk.Focus();
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Button_Enter(object sender, EventArgs e)
		{
			try
			{
				if (sender.GetType() == typeof(Button))
				{
					((Button)sender).BackColor = Color.Yellow;
				}
				else
				{
					if (sender.GetType() == typeof(TextBox))
					{
						((TextBox)sender).BackColor = Color.Yellow;
						((TextBox)sender).SelectAll();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("Button_Enter", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Button_Leave(object sender, EventArgs e)
		{
			try
			{
				if (sender.GetType() == typeof(Button))
				{
					((Button)sender).BackColor = Color.WhiteSmoke;
				}
				else
				{
					if (sender.GetType() == typeof(TextBox))
					{
						((TextBox)sender).BackColor = Color.WhiteSmoke;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("Button_Leave", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void chbSavePincode_Click(object sender, EventArgs e)
		{
			if (Settings.Default.BSBoxSavePincode != this.chbSavePincode.Checked)
			{
				if (this.chbSavePincode.Checked)
				{
					this.gbSavePingDisclaimer.Show();
					this.gbSavePingDisclaimer.BringToFront();
				}
				else
				{
					Settings.Default.BSBoxSavePincode = this.chbSavePincode.Checked;
					Settings.Default.Save();
					this.tbPincode.Focus();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPincode_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Return || keyCode == Keys.Right)
			{
				this.btnOk.Focus();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmOrderFormConfirm_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Escape)
			{
				this.result = DialogResult.Cancel;
				this.CloseMe();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSavePinAgree_Click(object sender, EventArgs e)
		{
			Settings.Default.BSBoxSavePincode = true;
			Settings.Default.Save();
			this.chbSavePincode.Checked = true;
			this.gbSavePingDisclaimer.Hide();
			this.tbPincode.Focus();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSavePinDisAgree_Click(object sender, EventArgs e)
		{
			Settings.Default.BSBoxSavePincode = false;
			Settings.Default.Save();
			this.chbSavePincode.Checked = false;
			this.gbSavePingDisclaimer.Hide();
			this.tbPincode.Focus();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
	}
}
