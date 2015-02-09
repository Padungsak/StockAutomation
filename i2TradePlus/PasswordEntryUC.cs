using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class PasswordEntryUC : UserControl
	{
		public enum ValidatePasswordResults
		{
			None,
			EmptyPassword,
			EmptyConfirmPassword,
			ShorterThenMinLength,
			LongerThenMaxLength,
			NotContentAlphabat,
			NotContentNumeric,
			NotContentSpecialChar,
			ConfirmPasswordNotMatched,
			IsValid,
			UserNotAuthorize
		}
		public const string ALPHABAT_PATTERN = "[a-zA-Z]";
		public const string NUMERIC_PATTERN = "[0-9]";
		public const string SPECIALCHAR_PATTERN = "!|#|\\*|@|\\|";
		public const int PASSWORD_MIN_LENGTH = 6;
		public const int PASSWORD_MAX_LENGTH = 20;
		public const int PINCODE_MIN_LENGTH = 4;
		private const string INITIAL_OLD_PASSWORD_TEXT = "Enter your old password, press ESC for cancel.";
		private const string INITIAL_NEWPASSWORD_TEXT = "Choose password and confirm, press ESC for cancel.";
		private readonly Color INITIAL_NEWPASSWORD_TEXT_COLOR = SystemColors.GrayText;
		private bool isInPasswordChoosing = false;
		private string originalPassword = string.Empty;
		private bool isAutoEncrypt = true;
		private bool isUseDefalutPattern = true;
		private int minimumPasswordLength = 6;
		private int maximumPasswordLength = 0;
		private int minimumPincodeLength = 4;
		private string passwordSetted = string.Empty;
		private bool isCustomer = true;
		private bool isPincode;
		private PasswordEntryUC.ValidatePasswordResults validate = PasswordEntryUC.ValidatePasswordResults.None;
		private bool isPasswordModified = false;
		private IContainer components = null;
		private Label lblPasswordText;
		private Label lblConfirmPasswordText;
		private TextBox txtPassword;
		private TextBox txtConfirmPassword;
		private ErrorProvider errpValidatePassword;
		private PictureBox pictureBoxConfirmPasswordOK;
		private PictureBox pictureBoxPasswordOK;
		private TextBox txtOldPassword;
		private Label lblOldPassword;
		private PictureBox pictureBoxOldPassword;
		private Label lblInfo;
		private Button btnSave;
		[DefaultValue(true)]
		public bool IsAutoEncrypt
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isAutoEncrypt;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isAutoEncrypt = value;
			}
		}
		[DefaultValue(true)]
		public bool IsUseDefalutPattern
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isUseDefalutPattern;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isUseDefalutPattern = value;
			}
		}
		[DefaultValue("Old Password : ")]
		public string OldPasswordLable
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lblOldPassword.Text;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.lblOldPassword.Text = value;
			}
		}
		[DefaultValue("New Password : ")]
		public string PasswordLable
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lblPasswordText.Text;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.lblPasswordText.Text = value;
			}
		}
		[DefaultValue("Confirm Password : ")]
		public string ConfirmPasswordLable
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lblConfirmPasswordText.Text;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.lblConfirmPasswordText.Text = value;
			}
		}
		[DefaultValue(6)]
		public int MinimumPasswordLength
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.minimumPasswordLength;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.minimumPasswordLength = value;
			}
		}
		[DefaultValue(0)]
		public int MaximumPasswordLength
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.maximumPasswordLength;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.maximumPasswordLength = value;
			}
		}
		[DefaultValue(4)]
		public int MinimumPincodeLength
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.minimumPincodeLength;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.minimumPincodeLength = value;
			}
		}
		[Browsable(false)]
		public string PasswordSetted
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.passwordSetted;
			}
		}
		public bool IsCustomer
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isCustomer;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isCustomer = value;
			}
		}
		public bool IsPincode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isPincode;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isPincode = value;
				this.lblInfo.Text = (this.isPincode ? "Your old Pincode is wrong." : "Your old Password is wrong.");
			}
		}
		[Browsable(false)]
		public bool IsPasswordModified
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isPasswordModified;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PasswordEntryUC()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ChangePasswordUC_Load(object sender, EventArgs e)
		{
			this.InitialControls();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtOldPassword_Validated(object sender, EventArgs e)
		{
			this.originalPassword = this.txtOldPassword.Text.Trim();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtPassword_Validating(object sender, CancelEventArgs e)
		{
			this.pictureBoxPasswordOK.Visible = false;
			if (this.isInPasswordChoosing)
			{
				this.validate = this.ValidatePassword(this.txtPassword.Text);
				if (this.validate != PasswordEntryUC.ValidatePasswordResults.IsValid)
				{
					DescriptionAttribute[] array = (DescriptionAttribute[])typeof(PasswordEntryUC.ValidatePasswordResults).GetField(this.validate.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
					if (array != null && array.Length > 0)
					{
						this.errpValidatePassword.SetError(this.txtPassword, array[0].Description);
					}
					else
					{
						this.errpValidatePassword.SetError(this.txtPassword, this.validate.ToString());
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtPassword_Validated(object sender, EventArgs e)
		{
			if (this.isInPasswordChoosing && this.validate == PasswordEntryUC.ValidatePasswordResults.IsValid)
			{
				this.pictureBoxPasswordOK.Visible = true;
				if (this.txtConfirmPassword.Text == "Choose password and confirm, press ESC for cancel.")
				{
					this.txtConfirmPassword.Text = string.Empty;
				}
				this.txtConfirmPassword.Font = new Font(this.txtConfirmPassword.Font.FontFamily, this.txtConfirmPassword.Font.Size, FontStyle.Regular);
				this.txtConfirmPassword.PasswordChar = '*';
				this.txtConfirmPassword.ReadOnly = false;
				this.txtConfirmPassword.Focus();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.errpValidatePassword.Clear();
			this.pictureBoxPasswordOK.Visible = false;
			this.InitialConfirmPasswordControl();
			if (Convert.ToInt32(e.KeyChar) == 27)
			{
				this.InitialControls();
			}
			else
			{
				if (Convert.ToInt32(e.KeyChar) == 13)
				{
					if (this.isInPasswordChoosing)
					{
						this.txtConfirmPassword.Focus();
					}
				}
				else
				{
					if (this.txtPassword.Text == "Choose password and confirm, press ESC for cancel.")
					{
						this.passwordSetted = string.Empty;
						this.StartChooseNewPassword();
					}
				}
			}
			this.lblInfo.Visible = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
		{
			this.pictureBoxConfirmPasswordOK.Visible = false;
			if (this.isInPasswordChoosing)
			{
				this.validate = this.ValidatePassword(this.txtPassword.Text, this.txtConfirmPassword.Text);
				if (this.validate != PasswordEntryUC.ValidatePasswordResults.IsValid)
				{
					DescriptionAttribute[] array = (DescriptionAttribute[])typeof(PasswordEntryUC.ValidatePasswordResults).GetField(this.validate.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
					if (array != null && array.Length > 0)
					{
						this.errpValidatePassword.SetError(this.txtConfirmPassword, array[0].Description);
					}
					else
					{
						this.errpValidatePassword.SetError(this.txtConfirmPassword, this.validate.ToString());
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtConfirmPassword_Validated(object sender, EventArgs e)
		{
			if (this.isInPasswordChoosing && this.validate == PasswordEntryUC.ValidatePasswordResults.IsValid)
			{
				this.pictureBoxConfirmPasswordOK.Visible = true;
				this.passwordSetted = this.txtPassword.Text;
				this.isPasswordModified = true;
				this.btnSave.Enabled = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.errpValidatePassword.Clear();
			this.pictureBoxConfirmPasswordOK.Visible = false;
			if (Convert.ToInt32(e.KeyChar) == 27)
			{
				this.InitialControls();
				this.txtPassword.Focus();
			}
			else
			{
				if (Convert.ToInt32(e.KeyChar) == 13)
				{
					if (this.isInPasswordChoosing)
					{
						this.lblConfirmPasswordText.Focus();
					}
				}
			}
			this.lblInfo.Visible = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitialControls()
		{
			this.originalPassword = string.Empty;
			this.passwordSetted = string.Empty;
			this.isInPasswordChoosing = false;
			this.isPasswordModified = false;
			this.InitialPasswordControl();
			this.InitialConfirmPasswordControl();
			this.txtPassword.SelectionStart = 0;
			this.txtPassword.SelectionLength = 0;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitialPasswordControl()
		{
			this.txtOldPassword.PasswordChar = (this.txtPassword.PasswordChar = '\0');
			this.txtOldPassword.Text = "Enter your old password, press ESC for cancel.";
			this.txtPassword.Text = "Choose password and confirm, press ESC for cancel.";
			this.txtOldPassword.ForeColor = (this.txtPassword.ForeColor = this.INITIAL_NEWPASSWORD_TEXT_COLOR);
			this.txtOldPassword.Font = (this.txtPassword.Font = new Font(this.txtPassword.Font.FontFamily, this.txtPassword.Font.Size, FontStyle.Italic));
			this.pictureBoxOldPassword.Visible = (this.pictureBoxPasswordOK.Visible = false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitialConfirmPasswordControl()
		{
			this.txtConfirmPassword.PasswordChar = '\0';
			this.txtConfirmPassword.Text = "Choose password and confirm, press ESC for cancel.";
			this.txtConfirmPassword.Font = new Font(this.txtConfirmPassword.Font.FontFamily, this.txtConfirmPassword.Font.Size, FontStyle.Italic);
			this.txtConfirmPassword.ReadOnly = true;
			this.pictureBoxConfirmPasswordOK.Visible = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StartEnterOldPassword()
		{
			this.txtOldPassword.PasswordChar = '*';
			this.txtOldPassword.Text = string.Empty;
			this.txtOldPassword.ForeColor = SystemColors.WindowText;
			this.txtOldPassword.Font = new Font(this.txtPassword.Font.FontFamily, this.txtPassword.Font.Size, FontStyle.Regular);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StartChooseNewPassword()
		{
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Text = string.Empty;
			this.txtPassword.ForeColor = SystemColors.WindowText;
			this.txtPassword.Font = new Font(this.txtPassword.Font.FontFamily, this.txtPassword.Font.Size, FontStyle.Regular);
			this.isInPasswordChoosing = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private PasswordEntryUC.ValidatePasswordResults ValidatePassword(string password)
		{
			PasswordEntryUC.ValidatePasswordResults result;
			if (this.isPincode)
			{
				if (this.MinimumPincodeLength > 0 && string.IsNullOrEmpty(password))
				{
					result = PasswordEntryUC.ValidatePasswordResults.EmptyPassword;
				}
				else
				{
					if (this.MinimumPincodeLength > 0 && password.Length < this.MinimumPincodeLength)
					{
						result = PasswordEntryUC.ValidatePasswordResults.ShorterThenMinLength;
					}
					else
					{
						result = PasswordEntryUC.ValidatePasswordResults.IsValid;
					}
				}
			}
			else
			{
				if (this.minimumPasswordLength > 0 && string.IsNullOrEmpty(password))
				{
					result = PasswordEntryUC.ValidatePasswordResults.EmptyPassword;
				}
				else
				{
					if (this.minimumPasswordLength > 0 && password.Length < this.minimumPasswordLength)
					{
						result = PasswordEntryUC.ValidatePasswordResults.ShorterThenMinLength;
					}
					else
					{
						if (this.maximumPasswordLength > 0 && password.Length > this.maximumPasswordLength)
						{
							result = PasswordEntryUC.ValidatePasswordResults.LongerThenMaxLength;
						}
						else
						{
							if (this.minimumPasswordLength > 0 && this.isUseDefalutPattern)
							{
								if (this.isCustomer)
								{
									if (!Regex.IsMatch(password, "[a-zA-Z]"))
									{
										result = PasswordEntryUC.ValidatePasswordResults.NotContentAlphabat;
									}
									else
									{
										if (!Regex.IsMatch(password, "[0-9]"))
										{
											result = PasswordEntryUC.ValidatePasswordResults.NotContentNumeric;
										}
										else
										{
											if (!Regex.IsMatch(password, "!|#|\\*|@|\\|"))
											{
												result = PasswordEntryUC.ValidatePasswordResults.NotContentSpecialChar;
											}
											else
											{
												result = PasswordEntryUC.ValidatePasswordResults.IsValid;
											}
										}
									}
								}
								else
								{
									result = PasswordEntryUC.ValidatePasswordResults.IsValid;
								}
							}
							else
							{
								result = PasswordEntryUC.ValidatePasswordResults.IsValid;
							}
						}
					}
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private PasswordEntryUC.ValidatePasswordResults ValidatePassword(string password, string confirmPassword)
		{
			return this.ValidatePassword(password, confirmPassword, false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private PasswordEntryUC.ValidatePasswordResults ValidatePassword(string password, string confirmPassword, bool isValidatePassword)
		{
			PasswordEntryUC.ValidatePasswordResults result;
			if (isValidatePassword)
			{
				result = this.ValidatePassword(password);
			}
			if (this.minimumPasswordLength > 0 && string.IsNullOrEmpty(confirmPassword))
			{
				result = PasswordEntryUC.ValidatePasswordResults.EmptyConfirmPassword;
			}
			else
			{
				if (password != confirmPassword)
				{
					result = PasswordEntryUC.ValidatePasswordResults.ConfirmPasswordNotMatched;
				}
				else
				{
					result = PasswordEntryUC.ValidatePasswordResults.IsValid;
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowPasswordChangedInfo(bool canChanged)
		{
			if (canChanged)
			{
				this.lblInfo.Text = "Password changed.";
				this.lblInfo.ForeColor = Color.Lime;
			}
			else
			{
				this.lblInfo.Text = "Your old password is wrong.";
				this.lblInfo.ForeColor = Color.Red;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ChangePasswordCustomer(string custAccID)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(this.originalPassword) && !string.IsNullOrEmpty(this.passwordSetted))
			{
				try
				{
					flag = ApplicationInfo.WebService.ChangeCustomerPassword(custAccID, this.originalPassword, this.passwordSetted);
					this.isPasswordModified = !flag;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			this.lblInfo.Visible = true;
			this.ShowPasswordChangedInfo(flag);
			return flag;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ChangePasswordTrader(string traderID)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(this.originalPassword) && !string.IsNullOrEmpty(this.passwordSetted))
			{
				try
				{
					flag = ApplicationInfo.WebService.ChangeTraderPassword(traderID, this.originalPassword, this.passwordSetted);
					this.isPasswordModified = !flag;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			this.lblInfo.Visible = true;
			if (flag)
			{
				this.lblInfo.Text = "บันทึก Password สำเร็จ.";
			}
			return flag;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ChangePINCODECustomer(string custAccID)
		{
			return false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtOldPassword_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (Convert.ToInt32(e.KeyChar) == 27)
			{
				this.InitialControls();
			}
			else
			{
				if (Convert.ToInt32(e.KeyChar) == 13)
				{
					this.txtPassword.Focus();
				}
				else
				{
					if (this.txtOldPassword.Text == "Enter your old password, press ESC for cancel.")
					{
						this.originalPassword = string.Empty;
						this.StartEnterOldPassword();
					}
				}
			}
			this.lblInfo.Visible = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.txtOldPassword.Width = (this.txtPassword.Width = (this.txtConfirmPassword.Width = base.Width - 150));
			this.pictureBoxOldPassword.Left = (this.pictureBoxPasswordOK.Left = (this.pictureBoxConfirmPasswordOK.Left = this.txtOldPassword.Left + this.txtOldPassword.Width));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSave_Click(object sender, EventArgs e)
		{
			bool flag = false;
			if (ApplicationInfo.UserLoginMode == "C")
			{
				if (this.IsPasswordModified)
				{
					flag = this.ChangePasswordCustomer(ApplicationInfo.UserLoginID);
				}
				flag = (!this.IsPasswordModified || this.ChangePINCODECustomer(ApplicationInfo.UserLoginID));
			}
			else
			{
				if (ApplicationInfo.UserLoginMode == "T")
				{
					flag = (!this.IsPasswordModified || this.ChangePasswordTrader(ApplicationInfo.UserLoginID));
				}
			}
			if (flag)
			{
				this.btnSave.Enabled = false;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PasswordEntryUC));
			this.lblPasswordText = new Label();
			this.lblConfirmPasswordText = new Label();
			this.txtPassword = new TextBox();
			this.txtConfirmPassword = new TextBox();
			this.errpValidatePassword = new ErrorProvider(this.components);
			this.pictureBoxPasswordOK = new PictureBox();
			this.pictureBoxConfirmPasswordOK = new PictureBox();
			this.txtOldPassword = new TextBox();
			this.lblOldPassword = new Label();
			this.pictureBoxOldPassword = new PictureBox();
			this.lblInfo = new Label();
			this.btnSave = new Button();
			((ISupportInitialize)this.errpValidatePassword).BeginInit();
			((ISupportInitialize)this.pictureBoxPasswordOK).BeginInit();
			((ISupportInitialize)this.pictureBoxConfirmPasswordOK).BeginInit();
			((ISupportInitialize)this.pictureBoxOldPassword).BeginInit();
			base.SuspendLayout();
			this.lblPasswordText.AutoSize = true;
			this.lblPasswordText.Location = new Point(3, 38);
			this.lblPasswordText.Name = "lblPasswordText";
			this.lblPasswordText.Size = new Size(62, 13);
			this.lblPasswordText.TabIndex = 4;
			this.lblPasswordText.Text = "Password : ";
			this.lblConfirmPasswordText.AutoSize = true;
			this.lblConfirmPasswordText.Location = new Point(3, 64);
			this.lblConfirmPasswordText.Name = "lblConfirmPasswordText";
			this.lblConfirmPasswordText.Size = new Size(99, 13);
			this.lblConfirmPasswordText.TabIndex = 5;
			this.lblConfirmPasswordText.Text = "Confirm password : ";
			this.txtPassword.ForeColor = SystemColors.GrayText;
			this.txtPassword.Location = new Point(103, 35);
			this.txtPassword.MaxLength = 12;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new Size(277, 20);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.Text = "Choose password and confirm,press ESC for cancel.";
			this.txtPassword.Validated += new EventHandler(this.txtPassword_Validated);
			this.txtPassword.KeyPress += new KeyPressEventHandler(this.txtPassword_KeyPress);
			this.txtPassword.Validating += new CancelEventHandler(this.txtPassword_Validating);
			this.txtConfirmPassword.Location = new Point(103, 61);
			this.txtConfirmPassword.MaxLength = 12;
			this.txtConfirmPassword.Name = "txtConfirmPassword";
			this.txtConfirmPassword.ReadOnly = true;
			this.txtConfirmPassword.Size = new Size(277, 20);
			this.txtConfirmPassword.TabIndex = 2;
			this.txtConfirmPassword.Validated += new EventHandler(this.txtConfirmPassword_Validated);
			this.txtConfirmPassword.KeyPress += new KeyPressEventHandler(this.txtConfirmPassword_KeyPress);
			this.txtConfirmPassword.Validating += new CancelEventHandler(this.txtConfirmPassword_Validating);
			this.errpValidatePassword.ContainerControl = this;
			this.pictureBoxPasswordOK.Image = (Image)componentResourceManager.GetObject("pictureBoxPasswordOK.Image");
			this.pictureBoxPasswordOK.Location = new Point(381, 35);
			this.pictureBoxPasswordOK.Name = "pictureBoxPasswordOK";
			this.pictureBoxPasswordOK.Size = new Size(16, 20);
			this.pictureBoxPasswordOK.TabIndex = 6;
			this.pictureBoxPasswordOK.TabStop = false;
			this.pictureBoxPasswordOK.Visible = false;
			this.pictureBoxConfirmPasswordOK.Image = (Image)componentResourceManager.GetObject("pictureBoxConfirmPasswordOK.Image");
			this.pictureBoxConfirmPasswordOK.Location = new Point(380, 61);
			this.pictureBoxConfirmPasswordOK.Name = "pictureBoxConfirmPasswordOK";
			this.pictureBoxConfirmPasswordOK.Size = new Size(16, 16);
			this.pictureBoxConfirmPasswordOK.SizeMode = PictureBoxSizeMode.AutoSize;
			this.pictureBoxConfirmPasswordOK.TabIndex = 7;
			this.pictureBoxConfirmPasswordOK.TabStop = false;
			this.txtOldPassword.ForeColor = SystemColors.GrayText;
			this.txtOldPassword.Location = new Point(103, 10);
			this.txtOldPassword.MaxLength = 12;
			this.txtOldPassword.Name = "txtOldPassword";
			this.txtOldPassword.Size = new Size(277, 20);
			this.txtOldPassword.TabIndex = 0;
			this.txtOldPassword.Text = "Enter current password ,press ESC for cancel.";
			this.txtOldPassword.Validated += new EventHandler(this.txtOldPassword_Validated);
			this.txtOldPassword.KeyPress += new KeyPressEventHandler(this.txtOldPassword_KeyPress);
			this.lblOldPassword.AutoSize = true;
			this.lblOldPassword.Location = new Point(3, 13);
			this.lblOldPassword.Name = "lblOldPassword";
			this.lblOldPassword.Size = new Size(81, 13);
			this.lblOldPassword.TabIndex = 3;
			this.lblOldPassword.Text = "Old Password : ";
			this.pictureBoxOldPassword.Image = (Image)componentResourceManager.GetObject("pictureBoxOldPassword.Image");
			this.pictureBoxOldPassword.Location = new Point(381, 10);
			this.pictureBoxOldPassword.Name = "pictureBoxOldPassword";
			this.pictureBoxOldPassword.Size = new Size(16, 20);
			this.pictureBoxOldPassword.TabIndex = 10;
			this.pictureBoxOldPassword.TabStop = false;
			this.pictureBoxOldPassword.Visible = false;
			this.lblInfo.AutoSize = true;
			this.lblInfo.ForeColor = Color.Red;
			this.lblInfo.Location = new Point(184, 97);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new Size(139, 13);
			this.lblInfo.TabIndex = 3;
			this.lblInfo.Text = "Your old password is wrong.";
			this.lblInfo.Visible = false;
			this.btnSave.Enabled = false;
			this.btnSave.Location = new Point(103, 90);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new Size(75, 23);
			this.btnSave.TabIndex = 11;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new EventHandler(this.btnSave_Click);
			base.AutoScaleMode = AutoScaleMode.None;
			this.AutoSize = true;
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.pictureBoxPasswordOK);
			base.Controls.Add(this.pictureBoxConfirmPasswordOK);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.pictureBoxOldPassword);
			base.Controls.Add(this.txtConfirmPassword);
			base.Controls.Add(this.txtOldPassword);
			base.Controls.Add(this.lblInfo);
			base.Controls.Add(this.lblOldPassword);
			base.Controls.Add(this.lblConfirmPasswordText);
			base.Controls.Add(this.lblPasswordText);
			base.Name = "PasswordEntryUC";
			base.Size = new Size(404, 120);
			base.Load += new EventHandler(this.ChangePasswordUC_Load);
			((ISupportInitialize)this.errpValidatePassword).EndInit();
			((ISupportInitialize)this.pictureBoxPasswordOK).EndInit();
			((ISupportInitialize)this.pictureBoxConfirmPasswordOK).EndInit();
			((ISupportInitialize)this.pictureBoxOldPassword).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
