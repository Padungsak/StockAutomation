using i2TradePlus.Information;
using i2TradePlus.ITSNetBusinessWS;
using i2TradePlus.Properties;
using ITSNet.Common.BIZ;
using STIControl;
using STIControl.ExpandTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class frmAutoTrade : ClientBaseForm, IRealtimeMessage
	{
		private IContainer components = null;
		private Panel panelOrderBox;
		private Panel panelStopOrder;
		private Label lbStopPriceLable;
		private ComboBox cbStopOrderPrice;
		private CheckBox chbLimit;
		private Label lbStopOrderField;
		private ComboBox cbStopOrderField;
		private ComboBox cbStock;
		private Label lbStock;
		private Button btnPriceDec;
		private Button btnPriceInc;
		private Button btnVolDec;
		private Button btnVolInc;
		private Label lbPin;
		private TextBox tbPin;
		private RadioButton rbCover;
		private RadioButton rbShort;
		private RadioButton rbSell;
		private RadioButton rbBuy;
		private Label lbPrice;
		private Label lbVolume;
		private TextBox tbVolume;
		private ComboBox cbPlatle;
		private Label lbPattern;
		private Panel panelTop;
		private ToolStrip tStripMenu;
		private ToolStripLabel tslbStatus;
		private ToolStripComboBox tscbStatus;
		private ToolStripLabel tslbStock;
		private ToolStripTextBox tstbStock;
		private ToolStripLabel tslbPrice;
		private ToolStripTextBox tstbPrice;
		private ToolStripLabel tslbSide;
		private ToolStripComboBox tscbSide;
		private ToolStripButton tsbtnClearCondition;
		private ToolStripButton tsbtnCancelOrder;
		private ToolStripButton tsbtnSearch;
		private ExpandGrid intzaStopOrder;
		private ComboBox cbPrice;
		private Button btnClear;
		private Button btnSendOrder;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmAutoTrade));
			ColumnItem columnItem = new ColumnItem();
			ColumnItem columnItem2 = new ColumnItem();
			ColumnItem columnItem3 = new ColumnItem();
			ColumnItem columnItem4 = new ColumnItem();
			ColumnItem columnItem5 = new ColumnItem();
			ColumnItem columnItem6 = new ColumnItem();
			ColumnItem columnItem7 = new ColumnItem();
			ColumnItem columnItem8 = new ColumnItem();
			ColumnItem columnItem9 = new ColumnItem();
			ColumnItem columnItem10 = new ColumnItem();
			ColumnItem columnItem11 = new ColumnItem();
			ColumnItem columnItem12 = new ColumnItem();
			ColumnItem columnItem13 = new ColumnItem();
			ColumnItem columnItem14 = new ColumnItem();
			ColumnItem columnItem15 = new ColumnItem();
			ColumnItem columnItem16 = new ColumnItem();
			ColumnItem columnItem17 = new ColumnItem();
			ColumnItem columnItem18 = new ColumnItem();
			this.panelOrderBox = new Panel();
			this.btnClear = new Button();
			this.btnSendOrder = new Button();
			this.cbPrice = new ComboBox();
			this.cbStock = new ComboBox();
			this.lbStock = new Label();
			this.btnPriceDec = new Button();
			this.tbPin = new TextBox();
			this.btnPriceInc = new Button();
			this.btnVolDec = new Button();
			this.btnVolInc = new Button();
			this.lbPin = new Label();
			this.rbCover = new RadioButton();
			this.rbShort = new RadioButton();
			this.rbSell = new RadioButton();
			this.rbBuy = new RadioButton();
			this.lbPrice = new Label();
			this.lbVolume = new Label();
			this.tbVolume = new TextBox();
			this.panelTop = new Panel();
			this.panelStopOrder = new Panel();
			this.lbStopPriceLable = new Label();
			this.cbStopOrderPrice = new ComboBox();
			this.chbLimit = new CheckBox();
			this.lbStopOrderField = new Label();
			this.cbStopOrderField = new ComboBox();
			this.lbPattern = new Label();
			this.cbPlatle = new ComboBox();
			this.tStripMenu = new ToolStrip();
			this.tslbStatus = new ToolStripLabel();
			this.tscbStatus = new ToolStripComboBox();
			this.tslbStock = new ToolStripLabel();
			this.tstbStock = new ToolStripTextBox();
			this.tslbPrice = new ToolStripLabel();
			this.tstbPrice = new ToolStripTextBox();
			this.tslbSide = new ToolStripLabel();
			this.tscbSide = new ToolStripComboBox();
			this.tsbtnClearCondition = new ToolStripButton();
			this.tsbtnCancelOrder = new ToolStripButton();
			this.tsbtnSearch = new ToolStripButton();
			this.intzaStopOrder = new ExpandGrid();
			this.panelOrderBox.SuspendLayout();
			this.panelTop.SuspendLayout();
			this.panelStopOrder.SuspendLayout();
			this.tStripMenu.SuspendLayout();
			base.SuspendLayout();
			this.panelOrderBox.BackColor = Color.FromArgb(45, 45, 45);
			this.panelOrderBox.Controls.Add(this.btnClear);
			this.panelOrderBox.Controls.Add(this.btnSendOrder);
			this.panelOrderBox.Controls.Add(this.cbPrice);
			this.panelOrderBox.Controls.Add(this.cbStock);
			this.panelOrderBox.Controls.Add(this.lbStock);
			this.panelOrderBox.Controls.Add(this.btnPriceDec);
			this.panelOrderBox.Controls.Add(this.tbPin);
			this.panelOrderBox.Controls.Add(this.btnPriceInc);
			this.panelOrderBox.Controls.Add(this.btnVolDec);
			this.panelOrderBox.Controls.Add(this.btnVolInc);
			this.panelOrderBox.Controls.Add(this.lbPin);
			this.panelOrderBox.Controls.Add(this.rbCover);
			this.panelOrderBox.Controls.Add(this.rbShort);
			this.panelOrderBox.Controls.Add(this.rbSell);
			this.panelOrderBox.Controls.Add(this.rbBuy);
			this.panelOrderBox.Controls.Add(this.lbPrice);
			this.panelOrderBox.Controls.Add(this.lbVolume);
			this.panelOrderBox.Controls.Add(this.tbVolume);
			this.panelOrderBox.Location = new Point(5, 69);
			this.panelOrderBox.Name = "panelOrderBox";
			this.panelOrderBox.Size = new Size(811, 34);
			this.panelOrderBox.TabIndex = 80;
			this.btnClear.AutoEllipsis = true;
			this.btnClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnClear.BackColor = Color.Transparent;
			this.btnClear.Cursor = Cursors.Hand;
			this.btnClear.FlatAppearance.BorderColor = Color.LightGray;
			this.btnClear.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnClear.FlatStyle = FlatStyle.Flat;
			this.btnClear.ForeColor = Color.WhiteSmoke;
			this.btnClear.Location = new Point(776, 8);
			this.btnClear.MaximumSize = new Size(58, 23);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new Size(54, 22);
			this.btnClear.TabIndex = 103;
			this.btnClear.TabStop = false;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnSendOrder.AutoEllipsis = true;
			this.btnSendOrder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnSendOrder.BackColor = Color.Transparent;
			this.btnSendOrder.Cursor = Cursors.Hand;
			this.btnSendOrder.FlatAppearance.BorderColor = Color.LightGray;
			this.btnSendOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnSendOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnSendOrder.FlatStyle = FlatStyle.Flat;
			this.btnSendOrder.ForeColor = Color.WhiteSmoke;
			this.btnSendOrder.Location = new Point(716, 8);
			this.btnSendOrder.MaximumSize = new Size(58, 23);
			this.btnSendOrder.Name = "btnSendOrder";
			this.btnSendOrder.Size = new Size(54, 22);
			this.btnSendOrder.TabIndex = 102;
			this.btnSendOrder.TabStop = false;
			this.btnSendOrder.Text = "Send";
			this.btnSendOrder.UseVisualStyleBackColor = false;
			this.cbPrice.AllowDrop = true;
			this.cbPrice.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"ATO",
				"ATC",
				"MP",
				"MO",
				"ML"
			});
			this.cbPrice.AutoCompleteMode = AutoCompleteMode.Append;
			this.cbPrice.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbPrice.BackColor = Color.FromArgb(224, 224, 224);
			this.cbPrice.FlatStyle = FlatStyle.Popup;
			this.cbPrice.ForeColor = Color.Black;
			this.cbPrice.FormattingEnabled = true;
			this.cbPrice.Items.AddRange(new object[]
			{
				"",
				"ATO",
				"ATC",
				"MP",
				"MO",
				"ML"
			});
			this.cbPrice.Location = new Point(540, 6);
			this.cbPrice.Name = "cbPrice";
			this.cbPrice.Size = new Size(57, 21);
			this.cbPrice.TabIndex = 101;
			this.cbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.cbStock.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.cbStock.BackColor = Color.FromArgb(224, 224, 224);
			this.cbStock.FlatStyle = FlatStyle.Popup;
			this.cbStock.ForeColor = Color.Black;
			this.cbStock.FormattingEnabled = true;
			this.cbStock.Location = new Point(268, 6);
			this.cbStock.MaxLength = 20;
			this.cbStock.Name = "cbStock";
			this.cbStock.Size = new Size(77, 21);
			this.cbStock.TabIndex = 0;
			this.cbStock.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbStock.Enter += new EventHandler(this.controlOrder_Enter);
			this.lbStock.AutoSize = true;
			this.lbStock.ForeColor = Color.LightGray;
			this.lbStock.Location = new Point(230, 11);
			this.lbStock.Margin = new Padding(2, 0, 2, 0);
			this.lbStock.Name = "lbStock";
			this.lbStock.Size = new Size(35, 13);
			this.lbStock.TabIndex = 100;
			this.lbStock.Text = "Stock";
			this.lbStock.TextAlign = ContentAlignment.MiddleLeft;
			this.btnPriceDec.FlatAppearance.BorderSize = 0;
			this.btnPriceDec.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPriceDec.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnPriceDec.FlatStyle = FlatStyle.Flat;
			this.btnPriceDec.Image = (Image)componentResourceManager.GetObject("btnPriceDec.Image");
			this.btnPriceDec.Location = new Point(519, 9);
			this.btnPriceDec.Name = "btnPriceDec";
			this.btnPriceDec.Size = new Size(15, 15);
			this.btnPriceDec.TabIndex = 95;
			this.btnPriceDec.UseVisualStyleBackColor = true;
			this.tbPin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tbPin.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tbPin.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPin.BorderStyle = BorderStyle.FixedSingle;
			this.tbPin.CharacterCasing = CharacterCasing.Upper;
			this.tbPin.Location = new Point(656, 8);
			this.tbPin.Margin = new Padding(2, 3, 2, 3);
			this.tbPin.MaxLength = 10;
			this.tbPin.Name = "tbPin";
			this.tbPin.PasswordChar = '*';
			this.tbPin.Size = new Size(55, 20);
			this.tbPin.TabIndex = 7;
			this.tbPin.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbPin.Enter += new EventHandler(this.controlOrder_Enter);
			this.btnPriceInc.FlatAppearance.BorderSize = 0;
			this.btnPriceInc.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPriceInc.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnPriceInc.FlatStyle = FlatStyle.Flat;
			this.btnPriceInc.Image = (Image)componentResourceManager.GetObject("btnPriceInc.Image");
			this.btnPriceInc.Location = new Point(603, 9);
			this.btnPriceInc.Name = "btnPriceInc";
			this.btnPriceInc.Size = new Size(15, 15);
			this.btnPriceInc.TabIndex = 94;
			this.btnPriceInc.UseVisualStyleBackColor = true;
			this.btnVolDec.FlatAppearance.BorderSize = 0;
			this.btnVolDec.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnVolDec.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnVolDec.FlatStyle = FlatStyle.Flat;
			this.btnVolDec.Image = (Image)componentResourceManager.GetObject("btnVolDec.Image");
			this.btnVolDec.Location = new Point(379, 9);
			this.btnVolDec.Name = "btnVolDec";
			this.btnVolDec.Size = new Size(15, 15);
			this.btnVolDec.TabIndex = 93;
			this.btnVolDec.UseVisualStyleBackColor = true;
			this.btnVolInc.FlatAppearance.BorderSize = 0;
			this.btnVolInc.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnVolInc.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnVolInc.FlatStyle = FlatStyle.Flat;
			this.btnVolInc.Image = (Image)componentResourceManager.GetObject("btnVolInc.Image");
			this.btnVolInc.Location = new Point(466, 9);
			this.btnVolInc.Name = "btnVolInc";
			this.btnVolInc.Size = new Size(15, 15);
			this.btnVolInc.TabIndex = 92;
			this.btnVolInc.UseVisualStyleBackColor = true;
			this.lbPin.AutoSize = true;
			this.lbPin.ForeColor = Color.LightGray;
			this.lbPin.Location = new Point(627, 11);
			this.lbPin.Margin = new Padding(2, 0, 2, 0);
			this.lbPin.Name = "lbPin";
			this.lbPin.Size = new Size(25, 13);
			this.lbPin.TabIndex = 90;
			this.lbPin.Text = "PIN";
			this.lbPin.TextAlign = ContentAlignment.MiddleLeft;
			this.rbCover.AutoSize = true;
			this.rbCover.ForeColor = Color.LightGray;
			this.rbCover.Location = new Point(167, 9);
			this.rbCover.Name = "rbCover";
			this.rbCover.Size = new Size(53, 17);
			this.rbCover.TabIndex = 88;
			this.rbCover.TabStop = true;
			this.rbCover.Text = "Cover";
			this.rbCover.UseVisualStyleBackColor = true;
			this.rbCover.Visible = false;
			this.rbShort.AutoSize = true;
			this.rbShort.ForeColor = Color.LightGray;
			this.rbShort.Location = new Point(112, 9);
			this.rbShort.Name = "rbShort";
			this.rbShort.Size = new Size(50, 17);
			this.rbShort.TabIndex = 87;
			this.rbShort.TabStop = true;
			this.rbShort.Text = "Short";
			this.rbShort.UseVisualStyleBackColor = true;
			this.rbShort.Visible = false;
			this.rbSell.AutoSize = true;
			this.rbSell.ForeColor = Color.LightGray;
			this.rbSell.Location = new Point(64, 8);
			this.rbSell.Name = "rbSell";
			this.rbSell.Size = new Size(42, 17);
			this.rbSell.TabIndex = 86;
			this.rbSell.TabStop = true;
			this.rbSell.Text = "Sell";
			this.rbSell.UseVisualStyleBackColor = true;
			this.rbBuy.AutoSize = true;
			this.rbBuy.ForeColor = Color.LightGray;
			this.rbBuy.Location = new Point(10, 7);
			this.rbBuy.Name = "rbBuy";
			this.rbBuy.Size = new Size(43, 17);
			this.rbBuy.TabIndex = 85;
			this.rbBuy.TabStop = true;
			this.rbBuy.Text = "Buy";
			this.rbBuy.UseVisualStyleBackColor = true;
			this.lbPrice.AutoSize = true;
			this.lbPrice.ForeColor = Color.LightGray;
			this.lbPrice.Location = new Point(485, 10);
			this.lbPrice.Margin = new Padding(2, 0, 2, 0);
			this.lbPrice.Name = "lbPrice";
			this.lbPrice.Size = new Size(31, 13);
			this.lbPrice.TabIndex = 13;
			this.lbPrice.Text = "Price";
			this.lbPrice.TextAlign = ContentAlignment.MiddleLeft;
			this.lbVolume.AutoSize = true;
			this.lbVolume.ForeColor = Color.LightGray;
			this.lbVolume.Location = new Point(352, 9);
			this.lbVolume.Margin = new Padding(2, 0, 2, 0);
			this.lbVolume.Name = "lbVolume";
			this.lbVolume.Size = new Size(22, 13);
			this.lbVolume.TabIndex = 11;
			this.lbVolume.Text = "Vol";
			this.lbVolume.TextAlign = ContentAlignment.MiddleLeft;
			this.tbVolume.BackColor = Color.FromArgb(224, 224, 224);
			this.tbVolume.BorderStyle = BorderStyle.FixedSingle;
			this.tbVolume.Location = new Point(399, 7);
			this.tbVolume.Margin = new Padding(2, 3, 2, 3);
			this.tbVolume.MaxLength = 10;
			this.tbVolume.Name = "tbVolume";
			this.tbVolume.Size = new Size(59, 20);
			this.tbVolume.TabIndex = 2;
			this.tbVolume.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbVolume.Enter += new EventHandler(this.controlOrder_Enter);
			this.panelTop.BackColor = Color.FromArgb(64, 64, 64);
			this.panelTop.Controls.Add(this.panelStopOrder);
			this.panelTop.Controls.Add(this.panelOrderBox);
			this.panelTop.Controls.Add(this.lbPattern);
			this.panelTop.Controls.Add(this.cbPlatle);
			this.panelTop.Dock = DockStyle.Top;
			this.panelTop.Location = new Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new Size(824, 125);
			this.panelTop.TabIndex = 113;
			this.panelStopOrder.BackColor = Color.Transparent;
			this.panelStopOrder.Controls.Add(this.lbStopPriceLable);
			this.panelStopOrder.Controls.Add(this.cbStopOrderPrice);
			this.panelStopOrder.Controls.Add(this.chbLimit);
			this.panelStopOrder.Controls.Add(this.lbStopOrderField);
			this.panelStopOrder.Controls.Add(this.cbStopOrderField);
			this.panelStopOrder.Location = new Point(6, 34);
			this.panelStopOrder.Name = "panelStopOrder";
			this.panelStopOrder.Size = new Size(810, 29);
			this.panelStopOrder.TabIndex = 110;
			this.panelStopOrder.Paint += new PaintEventHandler(this.panelStopOrder_Paint);
			this.lbStopPriceLable.AutoSize = true;
			this.lbStopPriceLable.ForeColor = Color.WhiteSmoke;
			this.lbStopPriceLable.Location = new Point(228, 7);
			this.lbStopPriceLable.Margin = new Padding(2, 0, 2, 0);
			this.lbStopPriceLable.Name = "lbStopPriceLable";
			this.lbStopPriceLable.Size = new Size(31, 13);
			this.lbStopPriceLable.TabIndex = 115;
			this.lbStopPriceLable.Text = "Price";
			this.lbStopPriceLable.TextAlign = ContentAlignment.MiddleLeft;
			this.cbStopOrderPrice.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbStopOrderPrice.BackColor = Color.FromArgb(224, 224, 224);
			this.cbStopOrderPrice.FlatStyle = FlatStyle.Popup;
			this.cbStopOrderPrice.ForeColor = Color.Black;
			this.cbStopOrderPrice.FormattingEnabled = true;
			this.cbStopOrderPrice.Location = new Point(270, 3);
			this.cbStopOrderPrice.Name = "cbStopOrderPrice";
			this.cbStopOrderPrice.Size = new Size(64, 21);
			this.cbStopOrderPrice.TabIndex = 114;
			this.cbStopOrderPrice.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbStopOrderPrice.Enter += new EventHandler(this.controlOrder_Enter);
			this.chbLimit.AutoSize = true;
			this.chbLimit.ForeColor = Color.WhiteSmoke;
			this.chbLimit.Location = new Point(342, 7);
			this.chbLimit.Margin = new Padding(2, 3, 0, 3);
			this.chbLimit.Name = "chbLimit";
			this.chbLimit.Size = new Size(103, 17);
			this.chbLimit.TabIndex = 111;
			this.chbLimit.Text = "Cancel End Day";
			this.chbLimit.UseVisualStyleBackColor = false;
			this.lbStopOrderField.AutoSize = true;
			this.lbStopOrderField.ForeColor = Color.WhiteSmoke;
			this.lbStopOrderField.Location = new Point(4, 7);
			this.lbStopOrderField.Margin = new Padding(2, 0, 2, 0);
			this.lbStopOrderField.Name = "lbStopOrderField";
			this.lbStopOrderField.Size = new Size(85, 13);
			this.lbStopOrderField.TabIndex = 109;
			this.lbStopOrderField.Text = "Order Conditions";
			this.lbStopOrderField.TextAlign = ContentAlignment.MiddleLeft;
			this.cbStopOrderField.AllowDrop = true;
			this.cbStopOrderField.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbStopOrderField.BackColor = Color.FromArgb(224, 224, 224);
			this.cbStopOrderField.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbStopOrderField.FlatStyle = FlatStyle.Popup;
			this.cbStopOrderField.ForeColor = Color.Black;
			this.cbStopOrderField.FormattingEnabled = true;
			this.cbStopOrderField.Items.AddRange(new object[]
			{
				"Last >=",
				"Last <=",
				"Last >= SMA(Day)",
				"Last <= SMA(Day)",
				"Last > Break High (Day)",
				"Last < Break High (Day)",
				"Last > Break Low (Day)",
				"Last < Break Low (Day)"
			});
			this.cbStopOrderField.Location = new Point(96, 3);
			this.cbStopOrderField.Name = "cbStopOrderField";
			this.cbStopOrderField.Size = new Size(123, 21);
			this.cbStopOrderField.TabIndex = 106;
			this.cbStopOrderField.SelectedIndexChanged += new EventHandler(this.cbStopOrderField_SelectedIndexChanged);
			this.cbStopOrderField.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbStopOrderField.Enter += new EventHandler(this.controlOrder_Enter);
			this.lbPattern.AutoSize = true;
			this.lbPattern.ForeColor = Color.LightGray;
			this.lbPattern.Location = new Point(6, 10);
			this.lbPattern.Margin = new Padding(2, 0, 2, 0);
			this.lbPattern.Name = "lbPattern";
			this.lbPattern.Size = new Size(47, 13);
			this.lbPattern.TabIndex = 111;
			this.lbPattern.Text = "Pattern :";
			this.lbPattern.TextAlign = ContentAlignment.MiddleLeft;
			this.cbPlatle.AllowDrop = true;
			this.cbPlatle.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.cbPlatle.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.cbPlatle.BackColor = Color.FromArgb(224, 224, 224);
			this.cbPlatle.FlatStyle = FlatStyle.Popup;
			this.cbPlatle.ForeColor = Color.Black;
			this.cbPlatle.FormattingEnabled = true;
			this.cbPlatle.Items.AddRange(new object[]
			{
				"Simple"
			});
			this.cbPlatle.Location = new Point(59, 6);
			this.cbPlatle.MaxLength = 20;
			this.cbPlatle.Name = "cbPlatle";
			this.cbPlatle.Size = new Size(77, 21);
			this.cbPlatle.TabIndex = 112;
			this.cbPlatle.Text = "Simple";
			this.tStripMenu.AllowMerge = false;
			this.tStripMenu.BackColor = Color.FromArgb(10, 10, 10);
			this.tStripMenu.CanOverflow = false;
			this.tStripMenu.GripMargin = new Padding(0);
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tslbStatus,
				this.tscbStatus,
				this.tslbStock,
				this.tstbStock,
				this.tslbPrice,
				this.tstbPrice,
				this.tslbSide,
				this.tscbSide,
				this.tsbtnClearCondition,
				this.tsbtnCancelOrder,
				this.tsbtnSearch
			});
			this.tStripMenu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tStripMenu.Location = new Point(0, 125);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Padding = new Padding(1, 2, 1, 1);
			this.tStripMenu.RenderMode = ToolStripRenderMode.Professional;
			this.tStripMenu.Size = new Size(824, 28);
			this.tStripMenu.TabIndex = 114;
			this.tStripMenu.Tag = "-1";
			this.tslbStatus.BackColor = Color.Transparent;
			this.tslbStatus.ForeColor = Color.Gainsboro;
			this.tslbStatus.Margin = new Padding(1);
			this.tslbStatus.Name = "tslbStatus";
			this.tslbStatus.Size = new Size(39, 23);
			this.tslbStatus.Text = "Status";
			this.tscbStatus.AutoCompleteCustomSource.AddRange(new string[]
			{
				"ALL",
				"O",
				"PO",
				"M",
				"C",
				"PX",
				"R",
				"X"
			});
			this.tscbStatus.AutoCompleteMode = AutoCompleteMode.Append;
			this.tscbStatus.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tscbStatus.BackColor = Color.FromArgb(45, 45, 45);
			this.tscbStatus.ForeColor = Color.LightGray;
			this.tscbStatus.Items.AddRange(new object[]
			{
				"ALL",
				"O",
				"PO",
				"M",
				"C",
				"PX",
				"R",
				"X"
			});
			this.tscbStatus.Margin = new Padding(1, 0, 1, 2);
			this.tscbStatus.MaxLength = 3;
			this.tscbStatus.Name = "tscbStatus";
			this.tscbStatus.Size = new Size(75, 23);
			this.tslbStock.BackColor = Color.Transparent;
			this.tslbStock.ForeColor = Color.Gainsboro;
			this.tslbStock.Margin = new Padding(1);
			this.tslbStock.Name = "tslbStock";
			this.tslbStock.Size = new Size(36, 23);
			this.tslbStock.Text = "Stock";
			this.tstbStock.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tstbStock.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tstbStock.BackColor = Color.FromArgb(45, 45, 45);
			this.tstbStock.BorderStyle = BorderStyle.FixedSingle;
			this.tstbStock.CharacterCasing = CharacterCasing.Upper;
			this.tstbStock.Font = new Font("Microsoft Sans Serif", 9f);
			this.tstbStock.ForeColor = Color.LightGray;
			this.tstbStock.Margin = new Padding(1, 0, 1, 2);
			this.tstbStock.MaxLength = 12;
			this.tstbStock.Name = "tstbStock";
			this.tstbStock.Size = new Size(80, 23);
			this.tslbPrice.BackColor = Color.Transparent;
			this.tslbPrice.ForeColor = Color.Gainsboro;
			this.tslbPrice.Margin = new Padding(1);
			this.tslbPrice.Name = "tslbPrice";
			this.tslbPrice.Size = new Size(33, 23);
			this.tslbPrice.Text = "Price";
			this.tstbPrice.BackColor = Color.FromArgb(45, 45, 45);
			this.tstbPrice.BorderStyle = BorderStyle.FixedSingle;
			this.tstbPrice.ForeColor = Color.LightGray;
			this.tstbPrice.Margin = new Padding(1, 0, 1, 2);
			this.tstbPrice.MaxLength = 8;
			this.tstbPrice.Name = "tstbPrice";
			this.tstbPrice.Size = new Size(65, 23);
			this.tslbSide.BackColor = Color.Transparent;
			this.tslbSide.ForeColor = Color.Gainsboro;
			this.tslbSide.Margin = new Padding(1);
			this.tslbSide.Name = "tslbSide";
			this.tslbSide.Size = new Size(29, 23);
			this.tslbSide.Text = "Side";
			this.tscbSide.AutoCompleteCustomSource.AddRange(new string[]
			{
				"ALL",
				"B",
				"S",
				"H",
				"C"
			});
			this.tscbSide.AutoCompleteMode = AutoCompleteMode.Append;
			this.tscbSide.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tscbSide.BackColor = Color.FromArgb(45, 45, 45);
			this.tscbSide.ForeColor = Color.LightGray;
			this.tscbSide.Items.AddRange(new object[]
			{
				"ALL",
				"B",
				"S",
				"H",
				"C"
			});
			this.tscbSide.Margin = new Padding(1, 0, 1, 2);
			this.tscbSide.MaxLength = 3;
			this.tscbSide.Name = "tscbSide";
			this.tscbSide.Size = new Size(75, 23);
			this.tsbtnClearCondition.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnClearCondition.ForeColor = Color.Gainsboro;
			this.tsbtnClearCondition.ImageTransparentColor = Color.Magenta;
			this.tsbtnClearCondition.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnClearCondition.Name = "tsbtnClearCondition";
			this.tsbtnClearCondition.Size = new Size(38, 22);
			this.tsbtnClearCondition.Text = "Clear";
			this.tsbtnClearCondition.ToolTipText = "Clear Condition";
			this.tsbtnCancelOrder.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnCancelOrder.ForeColor = Color.Tomato;
			this.tsbtnCancelOrder.Image = (Image)componentResourceManager.GetObject("tsbtnCancelOrder.Image");
			this.tsbtnCancelOrder.ImageTransparentColor = Color.Magenta;
			this.tsbtnCancelOrder.Name = "tsbtnCancelOrder";
			this.tsbtnCancelOrder.Size = new Size(63, 22);
			this.tsbtnCancelOrder.Text = "Cancel";
			this.tsbtnCancelOrder.ToolTipText = "Cancel Order";
			this.tsbtnSearch.Font = new Font("Microsoft Sans Serif", 9f);
			this.tsbtnSearch.ForeColor = Color.Gainsboro;
			this.tsbtnSearch.Image = Resources.refresh;
			this.tsbtnSearch.ImageTransparentColor = Color.Magenta;
			this.tsbtnSearch.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnSearch.Name = "tsbtnSearch";
			this.tsbtnSearch.Size = new Size(66, 22);
			this.tsbtnSearch.Text = "Search";
			this.intzaStopOrder.AllowDrop = true;
			this.intzaStopOrder.BackColor = Color.Black;
			this.intzaStopOrder.CanBlink = true;
			this.intzaStopOrder.CanDrag = false;
			this.intzaStopOrder.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Near;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.FontColor = Color.LightGray;
			columnItem.IsExpand = false;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "checkbox";
			columnItem.Text = "";
			columnItem.ValueFormat = FormatType.Bitmap;
			columnItem.Visible = true;
			columnItem.Width = 3;
			columnItem2.Alignment = StringAlignment.Center;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.FontColor = Color.LightGray;
			columnItem2.IsExpand = false;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "side";
			columnItem2.Text = "B/S";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 5;
			columnItem3.Alignment = StringAlignment.Near;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.FontColor = Color.LightGray;
			columnItem3.IsExpand = false;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "stock";
			columnItem3.Text = "Stock";
			columnItem3.ValueFormat = FormatType.Text;
			columnItem3.Visible = true;
			columnItem3.Width = 12;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.FontColor = Color.LightGray;
			columnItem4.IsExpand = false;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "volume";
			columnItem4.Text = "Volume";
			columnItem4.ValueFormat = FormatType.Volume;
			columnItem4.Visible = true;
			columnItem4.Width = 13;
			columnItem5.Alignment = StringAlignment.Far;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.FontColor = Color.LightGray;
			columnItem5.IsExpand = false;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "price";
			columnItem5.Text = "Price";
			columnItem5.ValueFormat = FormatType.Text;
			columnItem5.Visible = true;
			columnItem5.Width = 10;
			columnItem6.Alignment = StringAlignment.Near;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.FontColor = Color.LightGray;
			columnItem6.IsExpand = false;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "condition";
			columnItem6.Text = "Condition";
			columnItem6.ValueFormat = FormatType.Text;
			columnItem6.Visible = true;
			columnItem6.Width = 32;
			columnItem7.Alignment = StringAlignment.Center;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.FontColor = Color.LightGray;
			columnItem7.IsExpand = false;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "status";
			columnItem7.Text = "Status";
			columnItem7.ValueFormat = FormatType.Text;
			columnItem7.Visible = true;
			columnItem7.Width = 13;
			columnItem8.Alignment = StringAlignment.Center;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.FontColor = Color.LightGray;
			columnItem8.IsExpand = false;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "sent_time";
			columnItem8.Text = "Time";
			columnItem8.ValueFormat = FormatType.Text;
			columnItem8.Visible = true;
			columnItem8.Width = 12;
			columnItem9.Alignment = StringAlignment.Far;
			columnItem9.BackColor = Color.FromArgb(64, 64, 64);
			columnItem9.FontColor = Color.LightGray;
			columnItem9.IsExpand = false;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "pubvol";
			columnItem9.Text = "Publish";
			columnItem9.ValueFormat = FormatType.Volume;
			columnItem9.Visible = true;
			columnItem9.Width = 12;
			columnItem10.Alignment = StringAlignment.Center;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.FontColor = Color.LightGray;
			columnItem10.IsExpand = false;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "price_cond";
			columnItem10.Text = "Price Cond";
			columnItem10.ValueFormat = FormatType.Text;
			columnItem10.Visible = true;
			columnItem10.Width = 12;
			columnItem11.Alignment = StringAlignment.Center;
			columnItem11.BackColor = Color.FromArgb(64, 64, 64);
			columnItem11.FontColor = Color.LightGray;
			columnItem11.IsExpand = false;
			columnItem11.MyStyle = FontStyle.Regular;
			columnItem11.Name = "ttf";
			columnItem11.Text = "TTF";
			columnItem11.ValueFormat = FormatType.Volume;
			columnItem11.Visible = true;
			columnItem11.Width = 5;
			columnItem12.Alignment = StringAlignment.Center;
			columnItem12.BackColor = Color.FromArgb(64, 64, 64);
			columnItem12.FontColor = Color.LightGray;
			columnItem12.IsExpand = false;
			columnItem12.MyStyle = FontStyle.Regular;
			columnItem12.Name = "limit";
			columnItem12.Text = "Cancel End Day";
			columnItem12.ValueFormat = FormatType.Text;
			columnItem12.Visible = true;
			columnItem12.Width = 16;
			columnItem13.Alignment = StringAlignment.Center;
			columnItem13.BackColor = Color.FromArgb(64, 64, 64);
			columnItem13.FontColor = Color.LightGray;
			columnItem13.IsExpand = false;
			columnItem13.MyStyle = FontStyle.Regular;
			columnItem13.Name = "ref_no";
			columnItem13.Text = "Ref No.";
			columnItem13.ValueFormat = FormatType.Text;
			columnItem13.Visible = true;
			columnItem13.Width = 10;
			columnItem14.Alignment = StringAlignment.Center;
			columnItem14.BackColor = Color.FromArgb(64, 64, 64);
			columnItem14.FontColor = Color.LightGray;
			columnItem14.IsExpand = false;
			columnItem14.MyStyle = FontStyle.Regular;
			columnItem14.Name = "matched_time";
			columnItem14.Text = "S-Time";
			columnItem14.ValueFormat = FormatType.Text;
			columnItem14.Visible = true;
			columnItem14.Width = 10;
			columnItem15.Alignment = StringAlignment.Center;
			columnItem15.BackColor = Color.FromArgb(64, 64, 64);
			columnItem15.FontColor = Color.LightGray;
			columnItem15.IsExpand = false;
			columnItem15.MyStyle = FontStyle.Regular;
			columnItem15.Name = "order_no";
			columnItem15.Text = "Order No";
			columnItem15.ValueFormat = FormatType.Text;
			columnItem15.Visible = true;
			columnItem15.Width = 12;
			columnItem16.Alignment = StringAlignment.Near;
			columnItem16.BackColor = Color.FromArgb(64, 64, 64);
			columnItem16.FontColor = Color.LightGray;
			columnItem16.IsExpand = false;
			columnItem16.MyStyle = FontStyle.Regular;
			columnItem16.Name = "error";
			columnItem16.Text = "Error";
			columnItem16.ValueFormat = FormatType.Text;
			columnItem16.Visible = true;
			columnItem16.Width = 70;
			columnItem17.Alignment = StringAlignment.Near;
			columnItem17.BackColor = Color.FromArgb(64, 64, 64);
			columnItem17.FontColor = Color.LightGray;
			columnItem17.IsExpand = false;
			columnItem17.MyStyle = FontStyle.Regular;
			columnItem17.Name = "con_price";
			columnItem17.Text = "Price Cond";
			columnItem17.ValueFormat = FormatType.Text;
			columnItem17.Visible = false;
			columnItem17.Width = 10;
			columnItem18.Alignment = StringAlignment.Near;
			columnItem18.BackColor = Color.FromArgb(64, 64, 64);
			columnItem18.FontColor = Color.LightGray;
			columnItem18.IsExpand = false;
			columnItem18.MyStyle = FontStyle.Regular;
			columnItem18.Name = "con_operator";
			columnItem18.Text = "None";
			columnItem18.ValueFormat = FormatType.Text;
			columnItem18.Visible = false;
			columnItem18.Width = 10;
			this.intzaStopOrder.Columns.Add(columnItem);
			this.intzaStopOrder.Columns.Add(columnItem2);
			this.intzaStopOrder.Columns.Add(columnItem3);
			this.intzaStopOrder.Columns.Add(columnItem4);
			this.intzaStopOrder.Columns.Add(columnItem5);
			this.intzaStopOrder.Columns.Add(columnItem6);
			this.intzaStopOrder.Columns.Add(columnItem7);
			this.intzaStopOrder.Columns.Add(columnItem8);
			this.intzaStopOrder.Columns.Add(columnItem9);
			this.intzaStopOrder.Columns.Add(columnItem10);
			this.intzaStopOrder.Columns.Add(columnItem11);
			this.intzaStopOrder.Columns.Add(columnItem12);
			this.intzaStopOrder.Columns.Add(columnItem13);
			this.intzaStopOrder.Columns.Add(columnItem14);
			this.intzaStopOrder.Columns.Add(columnItem15);
			this.intzaStopOrder.Columns.Add(columnItem16);
			this.intzaStopOrder.Columns.Add(columnItem17);
			this.intzaStopOrder.Columns.Add(columnItem18);
			this.intzaStopOrder.CurrentScroll = 0;
			this.intzaStopOrder.Dock = DockStyle.Fill;
			this.intzaStopOrder.FocusItemIndex = -1;
			this.intzaStopOrder.ForeColor = Color.Black;
			this.intzaStopOrder.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaStopOrder.HeaderPctHeight = 100f;
			this.intzaStopOrder.IsAutoRepaint = true;
			this.intzaStopOrder.IsDrawGrid = true;
			this.intzaStopOrder.IsDrawHeader = true;
			this.intzaStopOrder.IsScrollable = true;
			this.intzaStopOrder.Location = new Point(0, 153);
			this.intzaStopOrder.MainColumn = "";
			this.intzaStopOrder.Name = "intzaStopOrder";
			this.intzaStopOrder.Rows = 0;
			this.intzaStopOrder.RowSelectColor = Color.Navy;
			this.intzaStopOrder.RowSelectType = 3;
			this.intzaStopOrder.ScrollChennelColor = Color.Gray;
			this.intzaStopOrder.Size = new Size(824, 397);
			this.intzaStopOrder.SortColumnName = "";
			this.intzaStopOrder.SortType = SortType.Desc;
			this.intzaStopOrder.TabIndex = 115;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(824, 550);
			base.Controls.Add(this.intzaStopOrder);
			base.Controls.Add(this.tStripMenu);
			base.Controls.Add(this.panelTop);
			base.Name = "frmAutoTrade";
			this.Text = "frmAutoTrade";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmAutoTrade_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmAutoTrade_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmAutoTrade_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmAutoTrade_IDoCustomSizeChanged);
			base.IDoMainFormKeyUp += new ClientBaseForm.OnFormKeyUpEventHandler(this.frmAutoTrade_IDoMainFormKeyUp);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmAutoTrade_IDoReActivated);
			base.Controls.SetChildIndex(this.panelTop, 0);
			base.Controls.SetChildIndex(this.tStripMenu, 0);
			base.Controls.SetChildIndex(this.intzaStopOrder, 0);
			this.panelOrderBox.ResumeLayout(false);
			this.panelOrderBox.PerformLayout();
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.panelStopOrder.ResumeLayout(false);
			this.panelStopOrder.PerformLayout();
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmAutoTrade()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmAutoTrade(Dictionary<string, object> propertiesValue) : base(propertiesValue)
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAutoTrade_IDoLoadData()
		{
			this.ReloadData();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAutoTrade_IDoShownDelay()
		{
			this.SetResize(true, true);
			base.Show();
			base.IsLoadingData = false;
			base.OpenedForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAutoTrade_IDoReActivated()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged, this.IsHeightChanged);
				base.Show();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAutoTrade_IDoCustomSizeChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged, this.IsHeightChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAutoTrade_IDoFontChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(true, true);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAutoTrade_IDoMainFormKeyUp(KeyEventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isWidthChanged, bool isHeightChanged)
		{
			try
			{
				if (isWidthChanged)
				{
					this.Font = Settings.Default.Default_Font;
					int num = 5;
					int num2 = num + 3;
					this.lbPattern.Location = new Point(5, 5);
					this.cbPlatle.Location = new Point(this.lbPattern.Right + 2, 3);
					this.cbStopOrderField.Location = new Point(this.lbStopOrderField.Right + 2, 3);
					this.lbStopPriceLable.Location = new Point(this.cbStopOrderField.Right + 5, 6);
					this.cbStopOrderPrice.Location = new Point(this.lbStopPriceLable.Right + 5, 3);
					this.chbLimit.Location = new Point(this.cbStopOrderPrice.Right + 5, 4);
					this.panelStopOrder.Height = this.cbStopOrderPrice.Bottom + this.cbStopOrderPrice.Top;
					this.rbBuy.Location = new Point(10, num + 1);
					this.rbSell.Location = new Point(this.rbBuy.Right + 2, this.rbSell.Top = num + 1);
					if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
					{
						this.rbShort.Location = new Point(this.rbSell.Right + 2, num + 1);
						this.rbCover.Location = new Point(this.rbShort.Right + 2, num + 1);
						this.lbStock.Location = new Point(this.rbCover.Right + 5, num2);
					}
					else
					{
						this.lbStock.Location = new Point(this.rbSell.Right + 5, num2);
					}
					this.cbStock.Location = new Point(this.lbStock.Right + 5, num - 1);
					this.lbVolume.Location = new Point(this.cbStock.Right + 10, num2);
					this.btnVolDec.Location = new Point(this.lbVolume.Right + 3, num2 + 1);
					this.tbVolume.Location = new Point(this.btnVolDec.Right + 2, num);
					this.btnVolInc.Location = new Point(this.tbVolume.Right + 2, num2 + 1);
					this.lbPrice.Location = new Point(this.btnVolInc.Right + 10, num2);
					this.btnPriceDec.Location = new Point(this.lbPrice.Right + 2, num2 + 1);
					this.cbPrice.Location = new Point(this.btnPriceDec.Right + 1, num - 1);
					this.btnPriceInc.Location = new Point(this.cbPrice.Right + 2, num2 + 1);
					this.btnClear.Location = new Point(this.panelTop.Width - this.btnClear.Width - 5, 2);
					this.btnSendOrder.Location = new Point(this.btnClear.Left - this.btnSendOrder.Width - 5, 2);
					this.tbPin.Location = new Point(this.btnSendOrder.Left - this.tbPin.Width - 5, 3);
					this.lbPin.Location = new Point(this.tbPin.Left - this.lbPin.Width - 5, 5);
					this.panelStopOrder.SetBounds(2, this.btnSendOrder.Bottom + 5, base.Width - 4, this.cbStopOrderField.Bottom + this.cbStopOrderField.Top);
					this.panelOrderBox.SetBounds(2, this.panelStopOrder.Bottom + 2, base.Width - 4, this.cbStock.Bottom + this.cbStock.Top);
					this.panelTop.Height = this.panelOrderBox.Bottom + 10;
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadData()
		{
			try
			{
				ApplicationInfo.WebAlertService.ViewStopOrderCompleted -= new ViewStopOrderCompletedEventHandler(this.MyWebService_ViewStopOrderCompleted);
				ApplicationInfo.WebAlertService.ViewStopOrderCompleted += new ViewStopOrderCompletedEventHandler(this.MyWebService_ViewStopOrderCompleted);
				ApplicationInfo.WebAlertService.ViewStopOrderAsync(ApplicationInfo.UserLoginID, "", 0);
			}
			catch (Exception ex)
			{
				this.ShowError("RequestWeb", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MyWebService_ViewStopOrderCompleted(object sender, ViewStopOrderCompletedEventArgs e)
		{
			try
			{
				ApplicationInfo.WebAlertService.ViewStopOrderCompleted -= new ViewStopOrderCompletedEventHandler(this.MyWebService_ViewStopOrderCompleted);
				if (e.Error == null)
				{
					using (DataSet dataSet = new DataSet())
					{
						MyDataHelper.StringToDataSet(e.Result.ToString(), dataSet);
						if (dataSet.Tables.Contains("ORDERS"))
						{
							this.intzaStopOrder.SortColumnName = string.Empty;
							this.intzaStopOrder.BeginUpdate();
							this.intzaStopOrder.Rows = dataSet.Tables["ORDERS"].Rows.Count;
							int num = 0;
							foreach (DataRow dr in dataSet.Tables["ORDERS"].Rows)
							{
								this.UpdateStopOrderToGrid(dr, num, false);
								num++;
							}
							this.intzaStopOrder.Redraw();
							if (this.intzaStopOrder.Rows > 0)
							{
								this.intzaStopOrder.Focus();
								this.intzaStopOrder.SetFocusItem(0);
							}
						}
						dataSet.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				this.intzaStopOrder.Redraw();
				this.ShowError("ViewStopOrderCompleted", ex);
			}
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UpdateStopOrderToGrid(DataRow dr, int i, bool isRedraw)
		{
			try
			{
				long num = 0L;
				long num2 = 0L;
				long num3 = 0L;
				string text = string.Empty;
				string text2 = string.Empty;
				string text3 = string.Empty;
				long.TryParse(dr["ref_no"].ToString(), out num3);
				RecordItem recordItem;
				if (i == -1)
				{
					recordItem = this.intzaStopOrder.Find("ref_no", num3.ToString());
					if (recordItem == null)
					{
						recordItem = this.intzaStopOrder.AddRecord(1, false);
					}
				}
				else
				{
					recordItem = this.intzaStopOrder.Records(i);
				}
				recordItem.Fields("ref_no").Text = num3;
				text = dr["ord_side"].ToString();
				recordItem.Fields("side").Text = text;
				StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[Convert.ToInt32(dr["stock"].ToString())];
				if (Utilities.PriceFormat(dr["ord_ttf"].ToString()) == string.Empty)
				{
					recordItem.Fields("stock").Text = stockInformation.Symbol;
				}
				else
				{
					recordItem.Fields("stock").Text = stockInformation.Symbol + "(" + dr["ord_ttf"].ToString() + ")";
				}
				long.TryParse(dr["ord_volume"].ToString(), out num);
				recordItem.Fields("volume").Text = num;
				long.TryParse(dr["ord_pubvol"].ToString(), out num2);
				recordItem.Fields("pubvol").Text = num2;
				recordItem.Fields("limit").Text = (Convert.ToBoolean(dr["limit"].ToString()) ? "Y" : "N");
				string text4 = dr["ord_condition"].ToString();
				if (text4 != null)
				{
					if (text4 == "I")
					{
						recordItem.Fields("price_cond").Text = "IOC";
						goto IL_2B4;
					}
					if (text4 == "F")
					{
						recordItem.Fields("price_cond").Text = "FOK";
						goto IL_2B4;
					}
				}
				recordItem.Fields("price_cond").Text = dr["ord_condition"].ToString();
				IL_2B4:
				text3 = dr["status"].ToString().Trim();
				text4 = text3;
				if (text4 != null)
				{
					if (!(text4 == "PO"))
					{
						if (!(text4 == "O"))
						{
							if (!(text4 == "F"))
							{
								if (!(text4 == "S"))
								{
									if (!(text4 == "M"))
									{
										if (text4 == "X")
										{
											text2 = "Cancel";
										}
									}
									else
									{
										text2 = "InComplete";
									}
								}
								else
								{
									text2 = "Sent";
								}
							}
							else
							{
								text2 = "Fail";
							}
						}
						else
						{
							text2 = "Pending";
						}
					}
					else
					{
						text2 = "Wait";
					}
				}
				recordItem.Fields("status").Text = text2;
				recordItem.Fields("price").Text = Utilities.PriceFormat(dr["ord_price"].ToString());
				recordItem.Fields("sent_time").Text = Utilities.GetTime(dr["time"].ToString());
				recordItem.Fields("matched_time").Text = Utilities.GetTime(dr["mtime"].ToString());
				string text5 = "  ";
				int num4 = 0;
				int.TryParse(dr["field_type"].ToString(), out num4);
				if (num4 == 1 || num4 == 4 || num4 == 5 || num4 == 6)
				{
					text5 += "Last";
				}
				else
				{
					text5 += "Unknow";
				}
				int num5 = 0;
				int.TryParse(dr["operator_type"].ToString(), out num5);
				if (num5 == 1)
				{
					text5 += " >= ";
				}
				else
				{
					if (num5 == 2)
					{
						text5 += " <= ";
					}
					else
					{
						if (num5 == 3)
						{
							text5 += " > ";
						}
						else
						{
							if (num5 == 4)
							{
								text5 += " < ";
							}
						}
					}
				}
				if (num4 == 4 || num4 == 5 || num4 == 6)
				{
					if (num4 == 4)
					{
						text5 += "SMA ";
					}
					else
					{
						if (num4 == 5)
						{
							text5 += "Break High ";
						}
						else
						{
							if (num4 == 6)
							{
								text5 += "Break Low ";
							}
						}
					}
					text5 += Utilities.PriceFormat(dr["price"].ToString());
					decimal num6 = 0m;
					decimal.TryParse(dr["sma_currprice"].ToString(), out num6);
					if (num6 > 0m)
					{
						text5 = text5 + " (@" + Utilities.PriceFormat(num6) + " )";
					}
					else
					{
						text5 += " (...)";
					}
				}
				else
				{
					text5 += Utilities.PriceFormat(dr["price"].ToString());
				}
				recordItem.Fields("condition").Text = text5;
				if (num4 == 1)
				{
					recordItem.Fields("con_price").Text = dr["price"].ToString();
				}
				else
				{
					if (num4 == 4)
					{
						recordItem.Fields("con_price").Text = dr["sma_currprice"].ToString();
					}
				}
				if (num5 == 1)
				{
					recordItem.Fields("con_operator").Text = ">=";
				}
				else
				{
					if (num5 == 2)
					{
						recordItem.Fields("con_operator").Text = "<=";
					}
					else
					{
						if (num5 == 3)
						{
							recordItem.Fields("con_operator").Text = ">";
						}
						else
						{
							if (num5 == 4)
							{
								recordItem.Fields("con_operator").Text = "<";
							}
						}
					}
				}
				recordItem.Fields("ttf").Text = dr["ord_ttf"].ToString().Trim();
				recordItem.Fields("order_no").Text = ((dr["order_number"].ToString() == "0") ? "" : dr["order_number"].ToString());
				recordItem.Fields("error").Text = dr["message"].ToString().Trim();
				recordItem.Fields("ref_no").FontColor = Color.White;
				if (text == "B")
				{
					recordItem.Fields("side").FontColor = Color.Lime;
				}
				else
				{
					if (text == "S")
					{
						recordItem.Fields("side").FontColor = Color.Red;
					}
					else
					{
						if (text == "C")
						{
							recordItem.Fields("side").FontColor = Color.Cyan;
						}
						else
						{
							if (text == "H")
							{
								recordItem.Fields("side").FontColor = Color.Pink;
							}
							else
							{
								recordItem.Fields("side").FontColor = Color.Yellow;
							}
						}
					}
				}
				recordItem.Fields("stock").FontColor = Color.LightGray;
				recordItem.Fields("volume").FontColor = Color.LightGray;
				recordItem.Fields("price").FontColor = Color.LightGray;
				recordItem.Fields("limit").FontColor = Color.LightGray;
				recordItem.Fields("sent_time").FontColor = Color.LightGray;
				recordItem.Fields("matched_time").FontColor = Color.LightGray;
				recordItem.Fields("status").FontColor = Color.Cyan;
				recordItem.Fields("order_no").FontColor = Color.Yellow;
				recordItem.Fields("error").FontColor = Color.Red;
				recordItem.Fields("condition").FontColor = Color.Yellow;
				recordItem.Fields("price_cond").FontColor = Color.LightGray;
				recordItem.Fields("pubvol").FontColor = Color.LightGray;
				recordItem.Fields("ttf").FontColor = Color.LightGray;
				if (text3 == "PO" || text3 == "O")
				{
					recordItem.Fields("checkbox").Text = "0";
				}
				else
				{
					recordItem.Fields("checkbox").Text = "";
				}
				recordItem.Changed = true;
				if (isRedraw)
				{
					this.intzaStopOrder.Redraw();
				}
			}
			catch (Exception ex)
			{
				this.intzaStopOrder.Redraw();
				this.ShowError("UpdateToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelStopOrder_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				e.Graphics.Clear(this.panelTop.BackColor);
				e.Graphics.DrawRectangle(Pens.DimGray, 0, 0, this.panelStopOrder.Width - 1, this.panelStopOrder.Height - 1);
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbStopOrderField_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cbStopOrderField.SelectedIndex > -1)
				{
					if (this.cbStopOrderField.Text.ToLower().IndexOf("sma") > 0)
					{
						this.lbStopPriceLable.Text = "Periods";
						this.cbStopOrderPrice.Items.Clear();
						this.cbStopOrderPrice.Text = string.Empty;
						for (int i = 2; i <= 200; i++)
						{
							if ((i >= 2 && i <= 50) || i == 75 || i == 200)
							{
								this.cbStopOrderPrice.Items.Add(i.ToString());
							}
						}
					}
					else
					{
						if (this.cbStopOrderField.Text.ToLower().IndexOf("break") > 0)
						{
							this.lbStopPriceLable.Text = "Periods";
							this.cbStopOrderPrice.Items.Clear();
							this.cbStopOrderPrice.Text = string.Empty;
							for (int j = 2; j <= 20; j++)
							{
								this.cbStopOrderPrice.Items.Add(j.ToString());
							}
						}
						else
						{
							this.lbStopPriceLable.Text = "Price";
							this.cbStopOrderPrice.Items.Clear();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("cbStopOrderField_SelectedIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Enter(object sender, EventArgs e)
		{
			try
			{
				((Control)sender).BackColor = Color.Yellow;
				((Control)sender).ForeColor = Color.Black;
				if (sender.GetType() == typeof(TextBox))
				{
					((TextBox)sender).SelectAll();
				}
				if (sender.Equals(this.cbPrice))
				{
					if (this.tbPin.Text == string.Empty && ApplicationInfo.UserPincodeLastEntry != string.Empty)
					{
						this.tbPin.Text = ApplicationInfo.UserPincodeLastEntry;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("controlOrder_Enter", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Leave(object sender, EventArgs e)
		{
			try
			{
				if (sender.GetType() == typeof(CheckBox))
				{
					((Control)sender).BackColor = Color.Transparent;
					if (this.panelOrderBox.BackColor == Color.Maroon || this.panelOrderBox.BackColor == Color.DarkGreen)
					{
						((Control)sender).ForeColor = Color.White;
					}
					else
					{
						((Control)sender).ForeColor = Color.Black;
					}
				}
				else
				{
					if (sender.GetType() == typeof(ComboBox))
					{
						((Control)sender).BackColor = Color.FromArgb(224, 224, 224);
						((Control)sender).ForeColor = Color.Black;
					}
					else
					{
						((Control)sender).BackColor = Color.FromArgb(224, 224, 224);
						((Control)sender).ForeColor = Color.Black;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("controlOrder_Leave", ex);
			}
		}
	}
}
