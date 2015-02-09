using i2TradePlus.Information;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus.WindowsForms
{
	public class frmTickerSetting : Form
	{
		public enum filterType
		{
			ALL_MARKET,
			SET_ONLY,
			TFEX_ONLY,
			SYMBOL,
			FAV1,
			FAV2,
			FAV3,
			FAV4,
			FAV5
		}
		private List<string> _symbolList = null;
		private frmTickerSetting.filterType _filterTickerType = frmTickerSetting.filterType.ALL_MARKET;
		private IContainer components = null;
		private Panel panel1;
		private RadioButton rdbtnBoard;
		private RadioButton rdbtnSymbol;
		private Label lbFilterSymbol;
		private Label label1;
		private Button btnAdd;
		private ListBox lstSymbol;
		private Button btnClear;
		private Button btnRemove;
		private Label lbBoard;
		private RadioButton rdbtnMarket;
		private TextBox cbSymbol;
		private ComboBox cbFavListType;
		private Label label2;
		private TabControl tabControl1;
		private TabPage tabMarket;
		private RadioButton rdbTFEX;
		private Label label3;
		private RadioButton rdbSET;
		private RadioButton rdbAllMarket;
		private TabPage tabSymbols;
		private TabPage tabFavorites;
		public List<string> SymbolList
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this._symbolList == null)
				{
					this._symbolList = new List<string>();
				}
				return this._symbolList;
			}
		}
		public frmTickerSetting.filterType FilterTickerType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._filterTickerType;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmTickerSetting()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmTickerSetting(frmTickerSetting.filterType filterType)
		{
			this.InitializeComponent();
			this._filterTickerType = filterType;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTickerSetting_Load(object sender, EventArgs e)
		{
			try
			{
				this._symbolList = ApplicationInfo.TickerStockList;
				this.label3.BringToFront();
				this.rdbtnMarket.Checked = false;
				this.rdbtnSymbol.Checked = false;
				this.rdbtnBoard.Checked = false;
				this.cbFavListType.SelectedIndex = 0;
				this.rdbSET.Enabled = true;
				this.rdbSET.Checked = true;
				if (ApplicationInfo.IsSupportTfex)
				{
					this.rdbAllMarket.Enabled = true;
					this.rdbTFEX.Enabled = true;
				}
				else
				{
					this.rdbAllMarket.Enabled = false;
					this.rdbTFEX.Enabled = false;
				}
				if (this._filterTickerType == frmTickerSetting.filterType.ALL_MARKET || this._filterTickerType == frmTickerSetting.filterType.SET_ONLY || this._filterTickerType == frmTickerSetting.filterType.TFEX_ONLY)
				{
					this.tabControl1.SelectedTab = this.tabMarket;
					this.rdbtnMarket.Checked = true;
					if (ApplicationInfo.IsSupportTfex)
					{
						if (this._filterTickerType == frmTickerSetting.filterType.ALL_MARKET)
						{
							this.rdbAllMarket.Checked = true;
						}
						else
						{
							if (this._filterTickerType == frmTickerSetting.filterType.SET_ONLY)
							{
								this.rdbSET.Checked = true;
							}
							else
							{
								if (this._filterTickerType == frmTickerSetting.filterType.TFEX_ONLY)
								{
									this.rdbTFEX.Checked = true;
								}
							}
						}
					}
					else
					{
						this.rdbSET.Checked = true;
						this.rdbAllMarket.Enabled = false;
						this.rdbTFEX.Enabled = false;
					}
				}
				else
				{
					if (this._filterTickerType == frmTickerSetting.filterType.SYMBOL)
					{
						this.tabControl1.SelectedTab = this.tabSymbols;
						this.rdbtnSymbol.Checked = true;
					}
					else
					{
						if (this._filterTickerType == frmTickerSetting.filterType.FAV1 || this._filterTickerType == frmTickerSetting.filterType.FAV2 || this._filterTickerType == frmTickerSetting.filterType.FAV3 || this._filterTickerType == frmTickerSetting.filterType.FAV4 || this._filterTickerType == frmTickerSetting.filterType.FAV5)
						{
							this.tabControl1.SelectedTab = this.tabFavorites;
							this.rdbtnBoard.Checked = true;
							if (this._filterTickerType == frmTickerSetting.filterType.FAV1)
							{
								this.cbFavListType.SelectedIndex = 0;
							}
							else
							{
								if (this._filterTickerType == frmTickerSetting.filterType.FAV2)
								{
									this.cbFavListType.SelectedIndex = 1;
								}
								else
								{
									if (this._filterTickerType == frmTickerSetting.filterType.FAV3)
									{
										this.cbFavListType.SelectedIndex = 2;
									}
									else
									{
										if (this._filterTickerType == frmTickerSetting.filterType.FAV4)
										{
											this.cbFavListType.SelectedIndex = 3;
										}
										else
										{
											if (this._filterTickerType == frmTickerSetting.filterType.FAV5)
											{
												this.cbFavListType.SelectedIndex = 4;
											}
										}
									}
								}
							}
						}
					}
				}
				if (this.cbSymbol.AutoCompleteCustomSource.Count == 0 && ApplicationInfo.StockAutoComp != null)
				{
					this.cbSymbol.AutoCompleteMode = AutoCompleteMode.Suggest;
					this.cbSymbol.AutoCompleteSource = AutoCompleteSource.CustomSource;
					if (ApplicationInfo.IsSupportTfex)
					{
						this.cbSymbol.AutoCompleteCustomSource = ApplicationInfo.MultiAutoComp;
					}
					else
					{
						this.cbSymbol.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
					}
				}
				if (this.lstSymbol.Items.Count > 0)
				{
					this.lstSymbol.Items.Clear();
				}
				StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this.cbSymbol.Text.Trim()];
				SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[this.cbSymbol.Text.Trim()];
				foreach (string current in this._symbolList)
				{
					if (!string.IsNullOrEmpty(current))
					{
						stockInformation = ApplicationInfo.StockInfo[current];
						seriesInformation = ApplicationInfo.SeriesInfo[current];
						if (stockInformation.Number > 0)
						{
							this.lstSymbol.Items.Add(current);
						}
						else
						{
							if (ApplicationInfo.IsSupportTfex)
							{
								this.lstSymbol.Items.Add(current);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmTickerSetting_Load", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this.cbSymbol.Text.Trim()];
				SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[this.cbSymbol.Text.Trim()];
				if (stockInformation.Number > 0)
				{
					if (!this.lstSymbol.Items.Contains(this.cbSymbol.Text.Trim()))
					{
						this.lstSymbol.Items.Add(this.cbSymbol.Text);
					}
					else
					{
						MessageBox.Show("Symbol is already existed.");
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(seriesInformation.Symbol) && ApplicationInfo.IsSupportTfex)
					{
						if (!this.lstSymbol.Items.Contains(this.cbSymbol.Text.Trim()))
						{
							this.lstSymbol.Items.Add(this.cbSymbol.Text);
						}
						else
						{
							MessageBox.Show("Symbol is already existed.");
						}
					}
					else
					{
						MessageBox.Show("Invalid Symbol!");
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnAdd_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnRemove_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstSymbol.SelectedIndex >= 0)
				{
					this.lstSymbol.Items.RemoveAt(this.lstSymbol.SelectedIndex);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnRemove_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnClear_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.lstSymbol.Items.Count > 0)
				{
					this.lstSymbol.Items.Clear();
				}
				this._symbolList.Clear();
			}
			catch (Exception ex)
			{
				this.ShowError("btnClear_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTickerSetting_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (this.rdbtnSymbol.Checked)
				{
					this._filterTickerType = frmTickerSetting.filterType.SYMBOL;
					if (this.lstSymbol.Items.Count > 0)
					{
						this._symbolList.Clear();
						foreach (string item in this.lstSymbol.Items)
						{
							this._symbolList.Add(item);
						}
					}
				}
				else
				{
					if (this.rdbtnBoard.Checked)
					{
						if (this.cbFavListType.Text == "Favorites 1")
						{
							this._filterTickerType = frmTickerSetting.filterType.FAV1;
						}
						else
						{
							if (this.cbFavListType.Text == "Favorites 2")
							{
								this._filterTickerType = frmTickerSetting.filterType.FAV2;
							}
							else
							{
								if (this.cbFavListType.Text == "Favorites 3")
								{
									this._filterTickerType = frmTickerSetting.filterType.FAV3;
								}
								else
								{
									if (this.cbFavListType.Text == "Favorites 4")
									{
										this._filterTickerType = frmTickerSetting.filterType.FAV4;
									}
									else
									{
										if (this.cbFavListType.Text == "Favorites 5")
										{
											this._filterTickerType = frmTickerSetting.filterType.FAV5;
										}
									}
								}
							}
						}
					}
					else
					{
						if (this.rdbtnMarket.Checked)
						{
							if (this.rdbAllMarket.Checked)
							{
								this._filterTickerType = frmTickerSetting.filterType.ALL_MARKET;
							}
							else
							{
								if (this.rdbSET.Checked)
								{
									this._filterTickerType = frmTickerSetting.filterType.SET_ONLY;
								}
								else
								{
									if (this.rdbTFEX.Checked)
									{
										this._filterTickerType = frmTickerSetting.filterType.TFEX_ONLY;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmTickerSetting_FormClosing", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbSymbol_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
			}
			catch (Exception ex)
			{
				this.ShowError("cbSymbol_KeyPress", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbSymbol_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.btnAdd.PerformClick();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void rdbtnFilterCheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.rdbtnSymbol.Checked)
				{
					this.tabControl1.SelectedTab = this.tabSymbols;
				}
				else
				{
					if (this.rdbtnBoard.Checked)
					{
						this.tabControl1.SelectedTab = this.tabFavorites;
					}
					else
					{
						this.tabControl1.SelectedTab = this.tabMarket;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("rdbtnFilterCheckedChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
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
			this.panel1 = new Panel();
			this.rdbtnMarket = new RadioButton();
			this.rdbtnBoard = new RadioButton();
			this.rdbtnSymbol = new RadioButton();
			this.cbFavListType = new ComboBox();
			this.label2 = new Label();
			this.lbBoard = new Label();
			this.cbSymbol = new TextBox();
			this.btnClear = new Button();
			this.btnRemove = new Button();
			this.btnAdd = new Button();
			this.lstSymbol = new ListBox();
			this.label1 = new Label();
			this.lbFilterSymbol = new Label();
			this.tabControl1 = new TabControl();
			this.tabMarket = new TabPage();
			this.rdbTFEX = new RadioButton();
			this.label3 = new Label();
			this.rdbSET = new RadioButton();
			this.rdbAllMarket = new RadioButton();
			this.tabSymbols = new TabPage();
			this.tabFavorites = new TabPage();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabMarket.SuspendLayout();
			this.tabSymbols.SuspendLayout();
			this.tabFavorites.SuspendLayout();
			base.SuspendLayout();
			this.panel1.BackColor = SystemColors.AppWorkspace;
			this.panel1.BorderStyle = BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.rdbtnMarket);
			this.panel1.Controls.Add(this.rdbtnBoard);
			this.panel1.Controls.Add(this.rdbtnSymbol);
			this.panel1.Location = new Point(4, 5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(383, 35);
			this.panel1.TabIndex = 0;
			this.rdbtnMarket.AutoSize = true;
			this.rdbtnMarket.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.rdbtnMarket.Location = new Point(8, 8);
			this.rdbtnMarket.Name = "rdbtnMarket";
			this.rdbtnMarket.Size = new Size(66, 17);
			this.rdbtnMarket.TabIndex = 2;
			this.rdbtnMarket.TabStop = true;
			this.rdbtnMarket.Text = "Market";
			this.rdbtnMarket.UseVisualStyleBackColor = true;
			this.rdbtnMarket.CheckedChanged += new EventHandler(this.rdbtnFilterCheckedChanged);
			this.rdbtnBoard.AutoSize = true;
			this.rdbtnBoard.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.rdbtnBoard.Location = new Point(170, 8);
			this.rdbtnBoard.Name = "rdbtnBoard";
			this.rdbtnBoard.Size = new Size(78, 17);
			this.rdbtnBoard.TabIndex = 1;
			this.rdbtnBoard.TabStop = true;
			this.rdbtnBoard.Text = "Favorites";
			this.rdbtnBoard.UseVisualStyleBackColor = true;
			this.rdbtnBoard.CheckedChanged += new EventHandler(this.rdbtnFilterCheckedChanged);
			this.rdbtnSymbol.AutoSize = true;
			this.rdbtnSymbol.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.rdbtnSymbol.Location = new Point(88, 8);
			this.rdbtnSymbol.Name = "rdbtnSymbol";
			this.rdbtnSymbol.Size = new Size(67, 17);
			this.rdbtnSymbol.TabIndex = 0;
			this.rdbtnSymbol.TabStop = true;
			this.rdbtnSymbol.Text = "Symbol";
			this.rdbtnSymbol.UseVisualStyleBackColor = true;
			this.rdbtnSymbol.CheckedChanged += new EventHandler(this.rdbtnFilterCheckedChanged);
			this.cbFavListType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbFavListType.FormattingEnabled = true;
			this.cbFavListType.Items.AddRange(new object[]
			{
				"Favorites 1",
				"Favorites 2",
				"Favorites 3",
				"Favorites 4",
				"Favorites 5"
			});
			this.cbFavListType.Location = new Point(130, 46);
			this.cbFavListType.Name = "cbFavListType";
			this.cbFavListType.Size = new Size(203, 24);
			this.cbFavListType.TabIndex = 2;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.label2.Location = new Point(59, 51);
			this.label2.Name = "label2";
			this.label2.Size = new Size(65, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Selection :";
			this.lbBoard.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.lbBoard.Location = new Point(9, 13);
			this.lbBoard.Name = "lbBoard";
			this.lbBoard.Size = new Size(349, 27);
			this.lbBoard.TabIndex = 0;
			this.lbBoard.Text = "Select favorites number.";
			this.cbSymbol.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.cbSymbol.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbSymbol.BorderStyle = BorderStyle.FixedSingle;
			this.cbSymbol.Location = new Point(67, 46);
			this.cbSymbol.Name = "cbSymbol";
			this.cbSymbol.Size = new Size(129, 23);
			this.cbSymbol.TabIndex = 7;
			this.cbSymbol.KeyDown += new KeyEventHandler(this.cbSymbol_KeyDown);
			this.cbSymbol.KeyPress += new KeyPressEventHandler(this.cbSymbol_KeyPress);
			this.btnClear.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.btnClear.Location = new Point(129, 138);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new Size(67, 23);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new EventHandler(this.btnClear_Click);
			this.btnRemove.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.btnRemove.Location = new Point(129, 109);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new Size(67, 23);
			this.btnRemove.TabIndex = 5;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
			this.btnAdd.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.btnAdd.Location = new Point(129, 80);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new Size(67, 23);
			this.btnAdd.TabIndex = 4;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.lstSymbol.FormattingEnabled = true;
			this.lstSymbol.ItemHeight = 16;
			this.lstSymbol.Location = new Point(209, 48);
			this.lstSymbol.Name = "lstSymbol";
			this.lstSymbol.ScrollAlwaysVisible = true;
			this.lstSymbol.Size = new Size(146, 116);
			this.lstSymbol.TabIndex = 3;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.label1.Location = new Point(6, 51);
			this.label1.Name = "label1";
			this.label1.Size = new Size(55, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Symbol :";
			this.lbFilterSymbol.AutoSize = true;
			this.lbFilterSymbol.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.lbFilterSymbol.Location = new Point(6, 12);
			this.lbFilterSymbol.Name = "lbFilterSymbol";
			this.lbFilterSymbol.Size = new Size(237, 13);
			this.lbFilterSymbol.TabIndex = 0;
			this.lbFilterSymbol.Text = "Enter your symbol and click 'Add' button.";
			this.tabControl1.Controls.Add(this.tabMarket);
			this.tabControl1.Controls.Add(this.tabSymbols);
			this.tabControl1.Controls.Add(this.tabFavorites);
			this.tabControl1.Location = new Point(3, 16);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(383, 209);
			this.tabControl1.TabIndex = 2;
			this.tabMarket.Controls.Add(this.rdbTFEX);
			this.tabMarket.Controls.Add(this.label3);
			this.tabMarket.Controls.Add(this.rdbSET);
			this.tabMarket.Controls.Add(this.rdbAllMarket);
			this.tabMarket.Location = new Point(4, 25);
			this.tabMarket.Name = "tabMarket";
			this.tabMarket.Padding = new Padding(3);
			this.tabMarket.Size = new Size(375, 180);
			this.tabMarket.TabIndex = 0;
			this.tabMarket.Text = "Market";
			this.tabMarket.UseVisualStyleBackColor = true;
			this.rdbTFEX.AutoSize = true;
			this.rdbTFEX.Enabled = false;
			this.rdbTFEX.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.rdbTFEX.Location = new Point(230, 49);
			this.rdbTFEX.Name = "rdbTFEX";
			this.rdbTFEX.Size = new Size(51, 17);
			this.rdbTFEX.TabIndex = 5;
			this.rdbTFEX.TabStop = true;
			this.rdbTFEX.Text = "TFEX";
			this.rdbTFEX.UseVisualStyleBackColor = true;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.label3.Location = new Point(16, 15);
			this.label3.Name = "label3";
			this.label3.Size = new Size(99, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Filter all symbol.";
			this.rdbSET.AutoSize = true;
			this.rdbSET.Enabled = false;
			this.rdbSET.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.rdbSET.Location = new Point(157, 49);
			this.rdbSET.Name = "rdbSET";
			this.rdbSET.Size = new Size(45, 17);
			this.rdbSET.TabIndex = 4;
			this.rdbSET.TabStop = true;
			this.rdbSET.Text = "SET";
			this.rdbSET.UseVisualStyleBackColor = true;
			this.rdbAllMarket.AutoSize = true;
			this.rdbAllMarket.Checked = true;
			this.rdbAllMarket.Enabled = false;
			this.rdbAllMarket.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			this.rdbAllMarket.Location = new Point(87, 49);
			this.rdbAllMarket.Name = "rdbAllMarket";
			this.rdbAllMarket.Size = new Size(39, 17);
			this.rdbAllMarket.TabIndex = 3;
			this.rdbAllMarket.TabStop = true;
			this.rdbAllMarket.Text = "All";
			this.rdbAllMarket.UseVisualStyleBackColor = true;
			this.tabSymbols.Controls.Add(this.cbSymbol);
			this.tabSymbols.Controls.Add(this.lbFilterSymbol);
			this.tabSymbols.Controls.Add(this.btnClear);
			this.tabSymbols.Controls.Add(this.label1);
			this.tabSymbols.Controls.Add(this.btnRemove);
			this.tabSymbols.Controls.Add(this.lstSymbol);
			this.tabSymbols.Controls.Add(this.btnAdd);
			this.tabSymbols.Location = new Point(4, 25);
			this.tabSymbols.Name = "tabSymbols";
			this.tabSymbols.Padding = new Padding(3);
			this.tabSymbols.Size = new Size(375, 180);
			this.tabSymbols.TabIndex = 1;
			this.tabSymbols.Text = "Symbol";
			this.tabSymbols.UseVisualStyleBackColor = true;
			this.tabFavorites.Controls.Add(this.lbBoard);
			this.tabFavorites.Controls.Add(this.cbFavListType);
			this.tabFavorites.Controls.Add(this.label2);
			this.tabFavorites.Location = new Point(4, 25);
			this.tabFavorites.Name = "tabFavorites";
			this.tabFavorites.Padding = new Padding(3);
			this.tabFavorites.Size = new Size(375, 180);
			this.tabFavorites.TabIndex = 2;
			this.tabFavorites.Text = "Favorites";
			this.tabFavorites.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.ActiveCaption;
			base.ClientSize = new Size(390, 229);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.tabControl1);
			this.Font = new Font("Tahoma", 9.75f);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.KeyPreview = true;
			base.Margin = new Padding(3, 4, 3, 4);
			base.MaximizeBox = false;
			base.Name = "frmTickerSetting";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Ticker Properties";
			base.Load += new EventHandler(this.frmTickerSetting_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmTickerSetting_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabMarket.ResumeLayout(false);
			this.tabMarket.PerformLayout();
			this.tabSymbols.ResumeLayout(false);
			this.tabSymbols.PerformLayout();
			this.tabFavorites.ResumeLayout(false);
			this.tabFavorites.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
