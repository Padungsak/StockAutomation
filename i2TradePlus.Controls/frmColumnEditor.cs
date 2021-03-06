using i2TradePlus.Properties;
using STIControl.ExpandTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus.Controls
{
	public class frmColumnEditor : Form
	{
		private IContainer components = null;
		private ListBox listBox1;
		private ListBox listBox2;
		private Button btnMoveUp;
		private Button btnMoveDown;
		private Label label1;
		private Label label2;
		private Button btnOk;
		private Button btnCancel;
		private Button btnReset;
		private Button btnSelected;
		private Button btnDisSelected;
		private TextBox tbDetail;
		private GroupBox groupBox1;
		private ExpandGrid intza = null;
		private List<ColumnItem> _adjColumns = null;
		public List<ColumnItem> AdjColumns
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._adjColumns;
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
			this.listBox1 = new ListBox();
			this.listBox2 = new ListBox();
			this.btnMoveUp = new Button();
			this.btnMoveDown = new Button();
			this.label1 = new Label();
			this.label2 = new Label();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			this.btnReset = new Button();
			this.btnSelected = new Button();
			this.btnDisSelected = new Button();
			this.tbDetail = new TextBox();
			this.groupBox1 = new GroupBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.listBox1.BorderStyle = BorderStyle.FixedSingle;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 15;
			this.listBox1.Location = new Point(8, 30);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new Size(165, 227);
			this.listBox1.TabIndex = 0;
			this.listBox1.MouseClick += new MouseEventHandler(this.listBox1_MouseClick);
			this.listBox2.BorderStyle = BorderStyle.FixedSingle;
			this.listBox2.FormattingEnabled = true;
			this.listBox2.ItemHeight = 15;
			this.listBox2.Location = new Point(230, 30);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new Size(165, 227);
			this.listBox2.TabIndex = 1;
			this.listBox2.MouseClick += new MouseEventHandler(this.listBox2_MouseClick);
			this.btnMoveUp.Image = Resources.Up1;
			this.btnMoveUp.Location = new Point(406, 30);
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.Size = new Size(34, 38);
			this.btnMoveUp.TabIndex = 2;
			this.btnMoveUp.UseVisualStyleBackColor = true;
			this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
			this.btnMoveDown.Image = Resources.Down;
			this.btnMoveDown.Location = new Point(406, 75);
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.Size = new Size(34, 35);
			this.btnMoveDown.TabIndex = 3;
			this.btnMoveDown.UseVisualStyleBackColor = true;
			this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(9, 8);
			this.label1.Name = "label1";
			this.label1.Size = new Size(56, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "Available";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(232, 8);
			this.label2.Name = "label2";
			this.label2.Size = new Size(55, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "Selected";
			this.btnOk.Location = new Point(94, 317);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new Size(90, 27);
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.btnCancel.Location = new Point(197, 317);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(90, 27);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnReset.Location = new Point(311, 317);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new Size(90, 27);
			this.btnReset.TabIndex = 8;
			this.btnReset.Text = "Use Default";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new EventHandler(this.btnReset_Click);
			this.btnSelected.Image = Resources.MoveNextHS;
			this.btnSelected.Location = new Point(181, 125);
			this.btnSelected.Name = "btnSelected";
			this.btnSelected.Size = new Size(37, 27);
			this.btnSelected.TabIndex = 9;
			this.btnSelected.UseVisualStyleBackColor = true;
			this.btnSelected.Click += new EventHandler(this.btnSelected_Click);
			this.btnDisSelected.Image = Resources.MovePreviousHS;
			this.btnDisSelected.Location = new Point(182, 159);
			this.btnDisSelected.Name = "btnDisSelected";
			this.btnDisSelected.Size = new Size(37, 27);
			this.btnDisSelected.TabIndex = 10;
			this.btnDisSelected.UseVisualStyleBackColor = true;
			this.btnDisSelected.Click += new EventHandler(this.btnDisSelected_Click);
			this.tbDetail.BorderStyle = BorderStyle.None;
			this.tbDetail.Location = new Point(36, 20);
			this.tbDetail.Name = "tbDetail";
			this.tbDetail.ReadOnly = true;
			this.tbDetail.Size = new Size(336, 14);
			this.tbDetail.TabIndex = 11;
			this.groupBox1.Controls.Add(this.tbDetail);
			this.groupBox1.Location = new Point(14, 267);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(381, 43);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Description";
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(450, 353);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.btnDisSelected);
			base.Controls.Add(this.btnSelected);
			base.Controls.Add(this.btnReset);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnMoveDown);
			base.Controls.Add(this.btnMoveUp);
			base.Controls.Add(this.listBox2);
			base.Controls.Add(this.listBox1);
			this.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "frmColumnEditor";
			this.Text = "Customize Columns";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmColumnEditor(ExpandGrid grid)
		{
			this.InitializeComponent();
			try
			{
				this.intza = grid;
				this.listBox1.Items.Clear();
				this.listBox2.Items.Clear();
				foreach (ColumnItem current in this.intza.Columns)
				{
					if (current.Visible)
					{
						this.listBox2.Items.Add(current.Text);
					}
					else
					{
						this.listBox1.Items.Add(current.Text);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSelected_Click(object sender, EventArgs e)
		{
			if (this.listBox1.SelectedIndex > -1)
			{
				this.listBox2.Items.Add(this.listBox1.Items[this.listBox1.SelectedIndex]);
				this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnDisSelected_Click(object sender, EventArgs e)
		{
			if (this.listBox2.SelectedIndex > -1)
			{
				this.listBox1.Items.Add(this.listBox2.Items[this.listBox2.SelectedIndex]);
				this.listBox2.Items.RemoveAt(this.listBox2.SelectedIndex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._adjColumns == null)
				{
					this._adjColumns = new List<ColumnItem>();
				}
				else
				{
					this._adjColumns.Clear();
				}
				ColumnItem[] array = new ColumnItem[this.intza.Columns.Count];
				this.intza.Columns.CopyTo(array);
				foreach (object current in this.listBox2.Items)
				{
					ColumnItem[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						ColumnItem columnItem = array2[i];
						if (columnItem.Text == current.ToString())
						{
							columnItem.Visible = true;
							this._adjColumns.Add(columnItem);
							break;
						}
					}
				}
				foreach (object current in this.listBox1.Items)
				{
					ColumnItem[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						ColumnItem columnItem = array2[i];
						if (columnItem.Text == current.ToString())
						{
							columnItem.Visible = false;
							this._adjColumns.Add(columnItem);
							break;
						}
					}
				}
				array = null;
			}
			catch (Exception ex)
			{
				this.ShowError("btnOk_Click", ex);
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnMoveUp_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.listBox2.SelectedIndex > 0 && this.listBox2.Items.Count > 1)
				{
					int selectedIndex = this.listBox2.SelectedIndex;
					string item = this.listBox2.Items[selectedIndex].ToString();
					this.listBox2.Items.RemoveAt(selectedIndex);
					this.listBox2.Items.Insert(selectedIndex - 1, item);
					this.listBox2.SelectedIndex = selectedIndex - 1;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnMoveUp_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnMoveDown_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.listBox2.SelectedIndex > 0 && this.listBox2.SelectedIndex < this.listBox2.Items.Count - 1 && this.listBox2.Items.Count > 1)
				{
					int selectedIndex = this.listBox2.SelectedIndex;
					string item = this.listBox2.Items[selectedIndex].ToString();
					this.listBox2.Items.RemoveAt(selectedIndex);
					this.listBox2.Items.Insert(selectedIndex + 1, item);
					this.listBox2.SelectedIndex = selectedIndex + 1;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnMoveDown_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void listBox1_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.listBox1.SelectedIndex > -1)
			{
				this.SetDetail(this.listBox1.Text);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void listBox2_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.listBox2.SelectedIndex > -1)
			{
				this.SetDetail(this.listBox2.Text);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetDetail(string symbol)
		{
			string str = string.Empty;
			string text = symbol.ToLower();
			switch (text)
			{
			case "stock":
				str = "Stock name";
				break;
			case "bidvol":
				str = "1st Bid Volume";
				break;
			case "offvol":
				str = "1st Offer Volume";
				break;
			case "bid":
				str = "1st Bid Price";
				break;
			case "offer":
				str = "1st Offer Price";
				break;
			case "avg":
				str = "Averange Price";
				break;
			case "bvol%":
				str = "Buy Volume Percentage";
				break;
			case "svol%":
				str = "Sell Volume Percentage";
				break;
			case "b/s":
				str = "Last Side";
				break;
			case "deals":
				str = "Total Main-Board Deals";
				break;
			case "volume":
				str = "Total Main-Board Volume";
				break;
			case "value":
				str = "Total Main-Board Value";
				break;
			case "high":
				str = "Highest Price";
				break;
			case "low":
				str = "Lowest Price";
				break;
			case "chg":
				str = "Change Price";
				break;
			case "%chg":
				str = "Change Price Percentage";
				break;
			case "prjcls":
				str = "Projected Close Price";
				break;
			case "prjopn":
				str = "Projected Open Price";
				break;
			}
			this.tbDetail.Text = symbol + " : " + str;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnReset_Click(object sender, EventArgs e)
		{
			try
			{
				List<string> list = new List<string>();
				list.Add("stock");
				list.Add("bidvol");
				list.Add("bid");
				list.Add("offer");
				list.Add("offvol");
				list.Add("last");
				list.Add("chg");
				list.Add("avg");
				list.Add("mvol");
				list.Add("mval");
				this.listBox1.Items.Clear();
				this.listBox2.Items.Clear();
				foreach (string current in list)
				{
					this.listBox2.Items.Add(this.intza.GetColumn(current).Text);
				}
				foreach (ColumnItem current2 in this.intza.Columns)
				{
					if (!list.Contains(current2.Name.ToLower()))
					{
						this.listBox1.Items.Add(current2.Text);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnReset_Click", ex);
			}
		}
	}
}
