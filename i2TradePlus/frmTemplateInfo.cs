using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class frmTemplateInfo : Form
	{
		public enum ShowMode
		{
			Save,
			SaveAs,
			Open,
			Delete,
			Option,
			Hotkeys
		}
		private frmTemplateInfo.ShowMode showMode = frmTemplateInfo.ShowMode.Open;
		private string templateName = string.Empty;
		private int templateHotkey;
		private bool isSetToDefaultTemplate = false;
		private bool isDatasChanged = false;
		private IContainer components = null;
		private Label label1;
		private TextBox txtTemplateName;
		private Button btnOK;
		private Button btnCancel;
		private ImageList imageListTemplateInfo;
		private CheckBox chkIsUseDefault;
		private Label lblplus;
		private ComboBox cmbModifierKey;
		private ComboBox cmbFunctionKey;
		private Label lblHotkey;
		private Label lbMessage;
		public string TemplateName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.templateName;
			}
		}
		public int TempalteHotkey
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.templateHotkey;
			}
		}
		public bool IsSetToDefaultTemplate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isSetToDefaultTemplate;
			}
		}
		public bool IsDatasChanged
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isDatasChanged;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmTemplateInfo(frmTemplateInfo.ShowMode showMode, string templateName, bool isSetToDefaultTemplate)
		{
			this.InitializeComponent();
			this.showMode = showMode;
			this.templateName = templateName;
			this.isSetToDefaultTemplate = isSetToDefaultTemplate;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTemplateInfo_Load(object sender, EventArgs e)
		{
			this.FillKeys();
			this.lbMessage.Text = "-";
			this.SetDisplayStyle();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnOK_Click(object sender, EventArgs e)
		{
			if (File.Exists(ApplicationInfo.GetTemplatePathByUser() + "\\" + this.txtTemplateName.Text + ".xml"))
			{
				if (this.showMode == frmTemplateInfo.ShowMode.Save || this.showMode == frmTemplateInfo.ShowMode.SaveAs)
				{
					if (MessageBox.Show("\"" + this.txtTemplateName.Text + "\" template already exists,would you like to replace it?", "Save template", MessageBoxButtons.YesNo) == DialogResult.No)
					{
						return;
					}
				}
			}
			else
			{
				if (this.showMode == frmTemplateInfo.ShowMode.Open)
				{
					this.lbMessage.Text = "\"" + this.txtTemplateName.Text + "\" template file not found.";
				}
			}
			if (this.showMode == frmTemplateInfo.ShowMode.Save || this.showMode == frmTemplateInfo.ShowMode.SaveAs || this.showMode == frmTemplateInfo.ShowMode.Hotkeys)
			{
				try
				{
					if (!string.IsNullOrEmpty(this.txtTemplateName.Text))
					{
						Keys keyData = this.GetKeyData();
						bool flag = false;
						string str = string.Empty;
						try
						{
							flag = HotKeyManager.CheckHotkey(keyData);
						}
						catch (Exception ex)
						{
							str = ex.Message;
						}
						if (!flag && (this.showMode == frmTemplateInfo.ShowMode.SaveAs || this.showMode == frmTemplateInfo.ShowMode.Hotkeys))
						{
							this.lbMessage.Text = this.DisplayKeyFormat(keyData) + ":" + str;
							this.lbMessage.ForeColor = Color.Red;
							return;
						}
						this.templateHotkey = (int)keyData;
					}
				}
				catch (Exception ex)
				{
					this.ShowError("btnOK_Click", ex);
					return;
				}
			}
			this.templateName = this.txtTemplateName.Text;
			this.isSetToDefaultTemplate = this.chkIsUseDefault.Checked;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.templateName = string.Empty;
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetDisplayStyle()
		{
			this.txtTemplateName.Text = this.templateName;
			this.chkIsUseDefault.Checked = this.isSetToDefaultTemplate;
			if (!string.IsNullOrEmpty(this.templateName))
			{
				Keys keyFromTemplateHotKey = HotKeyManager.GetKeyFromTemplateHotKey(this.templateName);
				if (keyFromTemplateHotKey != Keys.None)
				{
					this.DisplayKeysToCombobox(keyFromTemplateHotKey);
				}
			}
			switch (this.showMode)
			{
			case frmTemplateInfo.ShowMode.Save:
				this.chkIsUseDefault.Enabled = true;
				this.txtTemplateName.Enabled = true;
				break;
			case frmTemplateInfo.ShowMode.SaveAs:
				this.chkIsUseDefault.Enabled = true;
				break;
			case frmTemplateInfo.ShowMode.Option:
				this.btnCancel.Text = "Close";
				this.btnOK.Visible = false;
				this.txtTemplateName.ReadOnly = true;
				this.txtTemplateName.BackColor = SystemColors.Info;
				this.chkIsUseDefault.Enabled = false;
				break;
			case frmTemplateInfo.ShowMode.Hotkeys:
				this.chkIsUseDefault.Enabled = false;
				this.txtTemplateName.Enabled = false;
				this.cmbFunctionKey.Focus();
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void FillKeys()
		{
			this.cmbFunctionKey.Items.Clear();
			this.cmbModifierKey.Items.Clear();
			Keys[] array = HotKeyManager.ModifierKeys;
			for (int i = 0; i < array.Length; i++)
			{
				Keys keys = array[i];
				this.cmbModifierKey.Items.Add(keys.ToString().Replace(".None", string.Empty));
			}
			array = HotKeyManager.FunctionKeys;
			for (int i = 0; i < array.Length; i++)
			{
				Keys keys = array[i];
				this.cmbFunctionKey.Items.Add(keys);
			}
			this.cmbModifierKey.SelectedIndex = 0;
			this.cmbFunctionKey.SelectedIndex = 0;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string DisplayKeyFormat(Keys key)
		{
			string[] array = key.ToString().Split(new char[]
			{
				','
			});
			string text = (array.Length > 1) ? string.Format("{0}+{1}", array[1], array[0]) : array[0];
			return text.Trim();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DisplayKeysToCombobox(Keys keysCode)
		{
			try
			{
				string text = this.DisplayKeyFormat(keysCode);
				int num = text.IndexOf("+");
				if (num > 0)
				{
					this.cmbModifierKey.Text = text.Substring(0, num);
					this.cmbFunctionKey.Text = text.Substring(num + 1);
				}
				else
				{
					this.cmbFunctionKey.Text = text;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("DisplayKeysToCombobox", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Keys GetKeyData()
		{
			Keys keys = HotKeyManager.ModifierKeys[this.cmbModifierKey.SelectedIndex];
			if (this.cmbFunctionKey.SelectedIndex > -1)
			{
				keys |= HotKeyManager.FunctionKeys[this.cmbFunctionKey.SelectedIndex];
			}
			return keys;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string methodName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, methodName, ex.Message, ex.ToString()));
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmTemplateInfo));
			this.label1 = new Label();
			this.txtTemplateName = new TextBox();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.imageListTemplateInfo = new ImageList(this.components);
			this.chkIsUseDefault = new CheckBox();
			this.lblplus = new Label();
			this.cmbModifierKey = new ComboBox();
			this.cmbFunctionKey = new ComboBox();
			this.lblHotkey = new Label();
			this.lbMessage = new Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(23, 12);
			this.label1.Name = "label1";
			this.label1.Size = new Size(85, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Template Name:";
			this.txtTemplateName.Location = new Point(114, 9);
			this.txtTemplateName.Name = "txtTemplateName";
			this.txtTemplateName.Size = new Size(239, 20);
			this.txtTemplateName.TabIndex = 0;
			this.btnOK.Location = new Point(150, 69);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new Size(75, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.Location = new Point(243, 69);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.imageListTemplateInfo.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageListTemplateInfo.ImageStream");
			this.imageListTemplateInfo.TransparentColor = Color.Transparent;
			this.imageListTemplateInfo.Images.SetKeyName(0, "scrolldown");
			this.imageListTemplateInfo.Images.SetKeyName(1, "scrollup");
			this.chkIsUseDefault.AutoSize = true;
			this.chkIsUseDefault.Location = new Point(62, 73);
			this.chkIsUseDefault.Name = "chkIsUseDefault";
			this.chkIsUseDefault.Size = new Size(82, 17);
			this.chkIsUseDefault.TabIndex = 3;
			this.chkIsUseDefault.Text = "Use Default";
			this.chkIsUseDefault.UseVisualStyleBackColor = true;
			this.lblplus.AutoSize = true;
			this.lblplus.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 222);
			this.lblplus.Location = new Point(190, 41);
			this.lblplus.Name = "lblplus";
			this.lblplus.Size = new Size(16, 16);
			this.lblplus.TabIndex = 11;
			this.lblplus.Text = "+";
			this.cmbModifierKey.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbModifierKey.FormattingEnabled = true;
			this.cmbModifierKey.Location = new Point(111, 37);
			this.cmbModifierKey.Name = "cmbModifierKey";
			this.cmbModifierKey.Size = new Size(77, 21);
			this.cmbModifierKey.TabIndex = 1;
			this.cmbFunctionKey.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbFunctionKey.FormattingEnabled = true;
			this.cmbFunctionKey.Location = new Point(207, 37);
			this.cmbFunctionKey.Name = "cmbFunctionKey";
			this.cmbFunctionKey.Size = new Size(74, 21);
			this.cmbFunctionKey.TabIndex = 2;
			this.lblHotkey.AutoSize = true;
			this.lblHotkey.Location = new Point(59, 40);
			this.lblHotkey.Name = "lblHotkey";
			this.lblHotkey.Size = new Size(48, 13);
			this.lblHotkey.TabIndex = 8;
			this.lblHotkey.Text = "Hot Key:";
			this.lbMessage.BorderStyle = BorderStyle.FixedSingle;
			this.lbMessage.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbMessage.Location = new Point(1, 107);
			this.lbMessage.Name = "lbMessage";
			this.lbMessage.Size = new Size(373, 23);
			this.lbMessage.TabIndex = 13;
			this.lbMessage.Text = "-";
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new Size(376, 131);
			base.ControlBox = false;
			base.Controls.Add(this.lbMessage);
			base.Controls.Add(this.lblplus);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.cmbModifierKey);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.cmbFunctionKey);
			base.Controls.Add(this.chkIsUseDefault);
			base.Controls.Add(this.lblHotkey);
			base.Controls.Add(this.txtTemplateName);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "frmTemplateInfo";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Template Setup";
			base.Load += new EventHandler(this.frmTemplateInfo_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
