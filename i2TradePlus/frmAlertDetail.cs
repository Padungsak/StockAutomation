using i2TradePlus.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class frmAlertDetail : Form
	{
		private delegate void AddAlertItemCallback(AlertItem item);
		internal delegate void SetFontDialogCallBack(object formActive);
		private IContainer components = null;
		private ListView lvDetailAlert;
		private ColumnHeader SymbolCol;
		private ColumnHeader ExpressionCol;
		private ColumnHeader timeArriveCol;
		private ContextMenuStrip ctmsInformation;
		private ToolStripMenuItem tsmiClearDetail;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem tsmiCloesInfomation;
		private ToolStripMenuItem tsmiStartMonitor;
		private ToolStripMenuItem tsmiStopMonitor;
		private ToolStripMenuItem tsmiAlertOption;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripMenuItem tsmiSingleClear;
		private ToolStrip tsInformationDetail;
		private ToolStripDropDownButton tsdViewMunu;
		private ToolStripMenuItem tsmStockByPrice;
		private ToolStripMenuItem tsmStockInPlay;
		private ToolStripMenuItem tsmSaleByPrice;
		private ToolStripMenuItem tsmSaleByTime;
		private ToolStripMenuItem tsmViewOddLot;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripButton tsbStartMonitor;
		private ToolStripButton tsbStopMonitor;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripButton tsbClearRow;
		private ToolStripButton tsbClearAll;
		private ToolStripButton tsbAlertOption;
		private ToolStripSeparator toolStripSeparator7;
		private ToolStripButton tsbClose;
		private ToolStripMenuItem tsmiFontDialog;
		private ToolStripButton tsbFontDialog;
		private ToolStripLabel lbRowcount;
		private ToolStripLabel toolStripLabel1;
		private string symbolLink = string.Empty;
		private string messageTimeArrival;
		private frmSystemOption sytemOptionForm = null;
		internal string MessageTimeArrival
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageTimeArrival;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.messageTimeArrival = value;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmAlertDetail));
			this.lvDetailAlert = new ListView();
			this.SymbolCol = new ColumnHeader();
			this.ExpressionCol = new ColumnHeader();
			this.timeArriveCol = new ColumnHeader();
			this.ctmsInformation = new ContextMenuStrip(this.components);
			this.tsmiStartMonitor = new ToolStripMenuItem();
			this.tsmiStopMonitor = new ToolStripMenuItem();
			this.tsmiSingleClear = new ToolStripMenuItem();
			this.tsmiClearDetail = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsmiFontDialog = new ToolStripMenuItem();
			this.tsmiAlertOption = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.tsmiCloesInfomation = new ToolStripMenuItem();
			this.tsInformationDetail = new ToolStrip();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.tsdViewMunu = new ToolStripDropDownButton();
			this.tsmStockByPrice = new ToolStripMenuItem();
			this.tsmStockInPlay = new ToolStripMenuItem();
			this.tsmSaleByPrice = new ToolStripMenuItem();
			this.tsmSaleByTime = new ToolStripMenuItem();
			this.tsmViewOddLot = new ToolStripMenuItem();
			this.tsbStartMonitor = new ToolStripButton();
			this.tsbStopMonitor = new ToolStripButton();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.tsbClearRow = new ToolStripButton();
			this.tsbFontDialog = new ToolStripButton();
			this.tsbClearAll = new ToolStripButton();
			this.tsbAlertOption = new ToolStripButton();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.toolStripLabel1 = new ToolStripLabel();
			this.lbRowcount = new ToolStripLabel();
			this.tsbClose = new ToolStripButton();
			this.ctmsInformation.SuspendLayout();
			this.tsInformationDetail.SuspendLayout();
			base.SuspendLayout();
			this.lvDetailAlert.BackColor = Color.Black;
			this.lvDetailAlert.BorderStyle = BorderStyle.FixedSingle;
			this.lvDetailAlert.Columns.AddRange(new ColumnHeader[]
			{
				this.SymbolCol,
				this.ExpressionCol,
				this.timeArriveCol
			});
			this.lvDetailAlert.Dock = DockStyle.Fill;
			this.lvDetailAlert.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lvDetailAlert.ForeColor = Color.Yellow;
			this.lvDetailAlert.FullRowSelect = true;
			this.lvDetailAlert.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.lvDetailAlert.Location = new Point(0, 25);
			this.lvDetailAlert.Name = "lvDetailAlert";
			this.lvDetailAlert.Size = new Size(520, 156);
			this.lvDetailAlert.TabIndex = 2;
			this.lvDetailAlert.UseCompatibleStateImageBehavior = false;
			this.lvDetailAlert.View = View.Details;
			this.lvDetailAlert.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.lvDetailAlert_ItemSelectionChanged);
			this.SymbolCol.Text = "Symbol";
			this.SymbolCol.Width = 80;
			this.ExpressionCol.Text = "Expression";
			this.ExpressionCol.Width = 335;
			this.timeArriveCol.Text = "Times";
			this.timeArriveCol.Width = 80;
			this.ctmsInformation.Items.AddRange(new ToolStripItem[]
			{
				this.tsmiStartMonitor,
				this.tsmiStopMonitor,
				this.tsmiSingleClear,
				this.tsmiClearDetail,
				this.toolStripSeparator1,
				this.tsmiFontDialog,
				this.tsmiAlertOption,
				this.toolStripSeparator3,
				this.tsmiCloesInfomation
			});
			this.ctmsInformation.Name = "ctmsInformation";
			this.ctmsInformation.Size = new Size(149, 170);
			this.tsmiStartMonitor.Image = (Image)componentResourceManager.GetObject("tsmiStartMonitor.Image");
			this.tsmiStartMonitor.Name = "tsmiStartMonitor";
			this.tsmiStartMonitor.Size = new Size(148, 22);
			this.tsmiStartMonitor.Text = "Monitor";
			this.tsmiStartMonitor.Visible = false;
			this.tsmiStartMonitor.Click += new EventHandler(this.tsmiStartMonitor_Click);
			this.tsmiStopMonitor.Image = (Image)componentResourceManager.GetObject("tsmiStopMonitor.Image");
			this.tsmiStopMonitor.Name = "tsmiStopMonitor";
			this.tsmiStopMonitor.Size = new Size(148, 22);
			this.tsmiStopMonitor.Text = "&Stop";
			this.tsmiStopMonitor.Visible = false;
			this.tsmiStopMonitor.Click += new EventHandler(this.tsmiStopMonitor_Click);
			this.tsmiSingleClear.Name = "tsmiSingleClear";
			this.tsmiSingleClear.Size = new Size(148, 22);
			this.tsmiSingleClear.Text = "&Clear";
			this.tsmiSingleClear.Visible = false;
			this.tsmiSingleClear.Click += new EventHandler(this.tsmiSingleClear_Click);
			this.tsmiClearDetail.Image = (Image)componentResourceManager.GetObject("tsmiClearDetail.Image");
			this.tsmiClearDetail.Name = "tsmiClearDetail";
			this.tsmiClearDetail.Size = new Size(148, 22);
			this.tsmiClearDetail.Text = "&Clear All";
			this.tsmiClearDetail.Visible = false;
			this.tsmiClearDetail.Click += new EventHandler(this.tsmiClearDetail_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(145, 6);
			this.toolStripSeparator1.Visible = false;
			this.tsmiFontDialog.Image = (Image)componentResourceManager.GetObject("tsmiFontDialog.Image");
			this.tsmiFontDialog.Name = "tsmiFontDialog";
			this.tsmiFontDialog.Size = new Size(148, 22);
			this.tsmiFontDialog.Text = "Font..";
			this.tsmiFontDialog.Click += new EventHandler(this.tsmiFontDialog_Click);
			this.tsmiAlertOption.Image = (Image)componentResourceManager.GetObject("tsmiAlertOption.Image");
			this.tsmiAlertOption.Name = "tsmiAlertOption";
			this.tsmiAlertOption.Size = new Size(148, 22);
			this.tsmiAlertOption.Text = "Alert Option...";
			this.tsmiAlertOption.Visible = false;
			this.tsmiAlertOption.Click += new EventHandler(this.tsmiAlertOption_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(145, 6);
			this.tsmiCloesInfomation.Image = Resources.fileclose;
			this.tsmiCloesInfomation.Name = "tsmiCloesInfomation";
			this.tsmiCloesInfomation.Size = new Size(148, 22);
			this.tsmiCloesInfomation.Text = "&Close";
			this.tsmiCloesInfomation.Click += new EventHandler(this.tsmiCloesInfomation_Click);
			this.tsInformationDetail.GripStyle = ToolStripGripStyle.Hidden;
			this.tsInformationDetail.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripSeparator4,
				this.tsdViewMunu,
				this.tsbStartMonitor,
				this.tsbStopMonitor,
				this.toolStripSeparator5,
				this.tsbClearRow,
				this.tsbFontDialog,
				this.tsbClearAll,
				this.tsbAlertOption,
				this.toolStripSeparator7,
				this.toolStripLabel1,
				this.lbRowcount,
				this.tsbClose
			});
			this.tsInformationDetail.Location = new Point(0, 0);
			this.tsInformationDetail.Name = "tsInformationDetail";
			this.tsInformationDetail.RenderMode = ToolStripRenderMode.System;
			this.tsInformationDetail.Size = new Size(520, 25);
			this.tsInformationDetail.TabIndex = 3;
			this.tsInformationDetail.Text = "Information Detail";
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new Size(6, 25);
			this.toolStripSeparator4.Visible = false;
			this.tsdViewMunu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsdViewMunu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.tsmStockByPrice,
				this.tsmStockInPlay,
				this.tsmSaleByPrice,
				this.tsmSaleByTime,
				this.tsmViewOddLot
			});
			this.tsdViewMunu.Image = (Image)componentResourceManager.GetObject("tsdViewMunu.Image");
			this.tsdViewMunu.ImageTransparentColor = Color.Magenta;
			this.tsdViewMunu.Name = "tsdViewMunu";
			this.tsdViewMunu.Size = new Size(45, 22);
			this.tsdViewMunu.Text = "View";
			this.tsdViewMunu.ToolTipText = "View Menu Link...";
			this.tsdViewMunu.Visible = false;
			this.tsmStockByPrice.Name = "tsmStockByPrice";
			this.tsmStockByPrice.Size = new Size(142, 22);
			this.tsmStockByPrice.Text = "Stock Info";
			this.tsmStockInPlay.Name = "tsmStockInPlay";
			this.tsmStockInPlay.Size = new Size(142, 22);
			this.tsmStockInPlay.Text = "Stock In Play";
			this.tsmSaleByPrice.Name = "tsmSaleByPrice";
			this.tsmSaleByPrice.Size = new Size(142, 22);
			this.tsmSaleByPrice.Text = "Sale By Price";
			this.tsmSaleByTime.Name = "tsmSaleByTime";
			this.tsmSaleByTime.Size = new Size(142, 22);
			this.tsmSaleByTime.Text = "Sale By Time";
			this.tsmViewOddLot.Name = "tsmViewOddLot";
			this.tsmViewOddLot.Size = new Size(142, 22);
			this.tsmViewOddLot.Text = "View OddLot";
			this.tsbStartMonitor.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbStartMonitor.Image = (Image)componentResourceManager.GetObject("tsbStartMonitor.Image");
			this.tsbStartMonitor.ImageTransparentColor = Color.Magenta;
			this.tsbStartMonitor.Name = "tsbStartMonitor";
			this.tsbStartMonitor.Size = new Size(23, 22);
			this.tsbStartMonitor.Text = "Monitor";
			this.tsbStartMonitor.ToolTipText = "Start Monitoring.";
			this.tsbStartMonitor.Visible = false;
			this.tsbStartMonitor.Click += new EventHandler(this.tsmiStartMonitor_Click);
			this.tsbStopMonitor.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbStopMonitor.Image = (Image)componentResourceManager.GetObject("tsbStopMonitor.Image");
			this.tsbStopMonitor.ImageTransparentColor = Color.Magenta;
			this.tsbStopMonitor.Name = "tsbStopMonitor";
			this.tsbStopMonitor.Size = new Size(23, 22);
			this.tsbStopMonitor.Text = "Stop";
			this.tsbStopMonitor.ToolTipText = "Stop Monitoring.";
			this.tsbStopMonitor.Visible = false;
			this.tsbStopMonitor.Click += new EventHandler(this.tsmiStopMonitor_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new Size(6, 25);
			this.toolStripSeparator5.Visible = false;
			this.tsbClearRow.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbClearRow.Image = (Image)componentResourceManager.GetObject("tsbClearRow.Image");
			this.tsbClearRow.ImageTransparentColor = Color.Magenta;
			this.tsbClearRow.Name = "tsbClearRow";
			this.tsbClearRow.Size = new Size(38, 22);
			this.tsbClearRow.Text = "Clear";
			this.tsbClearRow.ToolTipText = "Clear row...";
			this.tsbClearRow.Visible = false;
			this.tsbClearRow.Click += new EventHandler(this.tsmiSingleClear_Click);
			this.tsbFontDialog.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbFontDialog.Image = (Image)componentResourceManager.GetObject("tsbFontDialog.Image");
			this.tsbFontDialog.ImageTransparentColor = Color.Magenta;
			this.tsbFontDialog.Name = "tsbFontDialog";
			this.tsbFontDialog.Size = new Size(23, 22);
			this.tsbFontDialog.Text = "Font...";
			this.tsbFontDialog.Visible = false;
			this.tsbFontDialog.Click += new EventHandler(this.tsbFontDialog_Click);
			this.tsbClearAll.Image = (Image)componentResourceManager.GetObject("tsbClearAll.Image");
			this.tsbClearAll.ImageTransparentColor = Color.Magenta;
			this.tsbClearAll.Name = "tsbClearAll";
			this.tsbClearAll.Size = new Size(54, 22);
			this.tsbClearAll.Text = "Clear";
			this.tsbClearAll.ToolTipText = "Clear All rows...";
			this.tsbClearAll.Click += new EventHandler(this.tsmiClearDetail_Click);
			this.tsbAlertOption.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbAlertOption.Image = (Image)componentResourceManager.GetObject("tsbAlertOption.Image");
			this.tsbAlertOption.ImageTransparentColor = Color.Magenta;
			this.tsbAlertOption.Name = "tsbAlertOption";
			this.tsbAlertOption.Size = new Size(23, 22);
			this.tsbAlertOption.Text = "Alert Option...";
			this.tsbAlertOption.Visible = false;
			this.tsbAlertOption.Click += new EventHandler(this.tsmiAlertOption_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new Size(6, 25);
			this.toolStripLabel1.BackColor = Color.Transparent;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new Size(67, 22);
			this.toolStripLabel1.Text = "Messages : ";
			this.lbRowcount.BackColor = Color.Transparent;
			this.lbRowcount.ForeColor = Color.Red;
			this.lbRowcount.Name = "lbRowcount";
			this.lbRowcount.Size = new Size(13, 22);
			this.lbRowcount.Text = "0";
			this.tsbClose.Alignment = ToolStripItemAlignment.Right;
			this.tsbClose.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbClose.Image = (Image)componentResourceManager.GetObject("tsbClose.Image");
			this.tsbClose.ImageTransparentColor = Color.Magenta;
			this.tsbClose.Name = "tsbClose";
			this.tsbClose.Size = new Size(23, 22);
			this.tsbClose.Text = "&Close";
			this.tsbClose.ToolTipText = "Close.";
			this.tsbClose.Click += new EventHandler(this.tsmiCloesInfomation_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(64, 64, 64);
			base.ClientSize = new Size(520, 181);
			this.ContextMenuStrip = this.ctmsInformation;
			base.ControlBox = false;
			base.Controls.Add(this.lvDetailAlert);
			base.Controls.Add(this.tsInformationDetail);
			this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.Name = "frmAlertDetail";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Price Alert on PC";
			base.FormClosing += new FormClosingEventHandler(this.frmAlertDetail_FormClosing);
			base.KeyDown += new KeyEventHandler(this.frmAlertDetail_KeyDown);
			this.ctmsInformation.ResumeLayout(false);
			this.tsInformationDetail.ResumeLayout(false);
			this.tsInformationDetail.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal frmAlertDetail()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal frmAlertDetail(AlertItem alertItem)
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAlertDetail_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				base.Close();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAlertDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			base.Hide();
			e.Cancel = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmiClearDetail_Click(object sender, EventArgs e)
		{
			this.lvDetailAlert.Items.Clear();
			this.symbolLink = string.Empty;
			AlertManager.Instance.MarkUnReadAll();
			this.UpdateRowCountLabel();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmiCloesInfomation_Click(object sender, EventArgs e)
		{
			base.Hide();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmiStopMonitor_Click(object sender, EventArgs e)
		{
			if (AlertManager.Instance.IsMonitoring)
			{
				AlertManager.Instance.IsMonitoring = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmiStartMonitor_Click(object sender, EventArgs e)
		{
			if (!AlertManager.Instance.IsMonitoring)
			{
				AlertManager.Instance.IsMonitoring = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmiAlertOption_Click(object sender, EventArgs e)
		{
			if (this.sytemOptionForm == null || this.sytemOptionForm.IsDisposed)
			{
				this.sytemOptionForm = new frmSystemOption();
			}
			this.sytemOptionForm.TopMost = true;
			this.sytemOptionForm.MdiParent = base.MdiParent;
			this.sytemOptionForm.Show();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void alertOptionForm_OnAlertClick()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void lvDetailAlert_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.symbolLink = e.Item.Text;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmiSingleClear_Click(object sender, EventArgs e)
		{
			if (this.lvDetailAlert.SelectedItems.Count != 0)
			{
				AlertItem item = (AlertItem)this.lvDetailAlert.Items[this.lvDetailAlert.SelectedItems[0].Index].Tag;
				AlertManager.Instance.MarkUnRead(this.symbolLink, item);
				this.lvDetailAlert.Items[this.lvDetailAlert.SelectedItems[0].Index].Remove();
				this.symbolLink = string.Empty;
				this.UpdateRowCountLabel();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmiFontDialog_Click(object sender, EventArgs e)
		{
			this.SetFontDialog(this);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbFontDialog_Click(object sender, EventArgs e)
		{
			this.SetFontDialog(this);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void AddAlertItem(AlertItem item)
		{
			try
			{
				if (this.lvDetailAlert.InvokeRequired)
				{
					this.lvDetailAlert.Invoke(new frmAlertDetail.AddAlertItemCallback(this.AddAlertItem), new object[]
					{
						item
					});
				}
				else
				{
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = item.Symbol;
					if (item.Field.ToLower() == "%change")
					{
						listViewItem.SubItems.Add(string.Concat(new string[]
						{
							item.Field,
							" >= ",
							Utilities.PriceFormat(item.Value),
							", Current = ",
							Utilities.PriceFormat(item.ValueMessageRealtime)
						}));
					}
					else
					{
						listViewItem.SubItems.Add(string.Concat(new string[]
						{
							item.Field,
							" ",
							item.Operator,
							" ",
							Utilities.PriceFormat(item.Value),
							", Current = ",
							Utilities.PriceFormat(item.ValueMessageRealtime)
						}));
					}
					listViewItem.SubItems.Add(DateTime.Now.ToLongTimeString());
					listViewItem.Tag = item;
					if (this.lvDetailAlert != null)
					{
						this.lvDetailAlert.Items.Insert(0, listViewItem);
					}
					this.UpdateRowCountLabel();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("AddAlertItem", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateRowCountLabel()
		{
			if (this.tsInformationDetail.InvokeRequired)
			{
				this.tsInformationDetail.Invoke(new MethodInvoker(this.UpdateRowCountLabel));
			}
			else
			{
				if (this.lvDetailAlert == null)
				{
					this.lbRowcount.Text = "0";
				}
				else
				{
					this.lbRowcount.Text = this.lvDetailAlert.Items.Count.ToString();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetFontDialog(object formActive)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmAlertDetail.SetFontDialogCallBack(this.SetFontDialog), new object[]
				{
					formActive
				});
			}
			else
			{
				if (formActive != null)
				{
					FontDialog fontDialog = new FontDialog();
					if (formActive.GetType() == typeof(frmAlertDetail))
					{
						fontDialog.Font = (formActive as frmAlertDetail).Controls[0].Font;
					}
					DialogResult dialogResult = fontDialog.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						foreach (Control control in (formActive as Form).Controls)
						{
							if (control.GetType() == typeof(Label))
							{
								control.Font = fontDialog.Font;
							}
							else
							{
								if (control.GetType() == typeof(ListView))
								{
									control.Font = fontDialog.Font;
								}
							}
							control.Refresh();
						}
						(formActive as Form).Invalidate();
					}
				}
			}
		}
	}
}
