using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace STIControl.SortTableGrid
{
	[Designer(typeof(SortTableGridDesigner))]
	public class SortGrid : UserControl, IColumnContainer
	{
		public delegate void ItemDragDropEventHandler(object sender, TableMouseEventArgs e, string dragValue);
		public delegate void TableMouseClickEventHandler(object sender, TableMouseEventArgs e);
		public delegate void TableMouseDoubleClickEventHandler(object sender, TableMouseEventArgs e);
		public delegate void TableKeyDownEventHandler(KeyEventArgs e);
		public delegate void TableMouseMoveEventHandler(TableMouseEventArgs e);
		public delegate void TableFocusIndexChangedEventHandler(object sender, int Index);
		private delegate void EndUpdateCallBack(int recordIndex);
		public const string STAR_TAG_FIELD = "@F";
		public static int ColumnID;
		private bool isAutoRepaint = true;
		private StringFormat stringFormat;
		private SortType sortType = SortType.Desc;
		private string sortColumeName = string.Empty;
		private string mainColumn = string.Empty;
		private Pen gridColor = new Pen(Color.FromArgb(45, 45, 45));
		private bool isMouseDown;
		private bool isFirstMouseMove;
		private Point mousePoint = default(Point);
		private List<RecordItem> _records;
		private Timer tmBlink;
		private Scrollbar vScrollbar;
		private Timer timerDragdrop;
		private bool isDraging;
		private FlakeDlg dragFlake;
		private FieldItem dragItem;
		private float headerPctHeight = 100f;
		private bool isScrollable;
		private bool isDrawFullRow;
		private bool isDrawGrid = true;
		private int rowSelectType;
		private Brush rowSelectBrush = Brushes.CadetBlue;
		private Color rowSelectColor = Color.CadetBlue;
		private bool isDrawHeader = true;
		private List<ColumnItem> _columns;
		private int _rowsVisible;
		private float _columnHeight;
		private float _rowHeight;
		private int _rowCount;
		private bool canBlink = true;
		private int focusItemIndex = -1;
		private bool canDrag;
		private bool isDroped;
		private bool canGetMouseMove;
		private int _currentScroll;
		private Brush _brushNoSelect = new SolidBrush(Color.FromArgb(64, 64, 64));
		private FieldItem _flForDraw;
		private float _beginY;
		private int _startIndexDrawing;
		private int _endIndexDrawing;
		private bool isPainting;
		private int _sY;
		private bool _hScroll;
		private bool isControlFocus;
        public  SortGrid.ItemDragDropEventHandler _ItemDragDrop;
		public event SortGrid.ItemDragDropEventHandler ItemDragDrop
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ItemDragDrop = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ItemDragDrop = value;
			}
		}
        public SortGrid.TableMouseClickEventHandler _TableMouseClick;
		public event SortGrid.TableMouseClickEventHandler TableMouseClick
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._TableMouseClick += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._TableMouseClick -= value;
			}
		}
        public SortGrid.TableMouseDoubleClickEventHandler _TableMouseDoubleClick;
		public event SortGrid.TableMouseDoubleClickEventHandler TableMouseDoubleClick
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._TableMouseDoubleClick += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._TableMouseDoubleClick -= value;
			}
		}
        public SortGrid.TableKeyDownEventHandler _TableKeyDown;
		public event SortGrid.TableKeyDownEventHandler TableKeyDown
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._TableKeyDown += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._TableKeyDown -= value;
			}
		}
        public SortGrid.TableMouseMoveEventHandler _TableMouseMove;
		public event SortGrid.TableMouseMoveEventHandler TableMouseMove
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._TableMouseMove += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._TableMouseMove -= value;
			}
		}
        public SortGrid.TableFocusIndexChangedEventHandler _TableFocusIndexChanged;
		public event SortGrid.TableFocusIndexChangedEventHandler TableFocusIndexChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._TableFocusIndexChanged += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._TableFocusIndexChanged -= value;
			}
		}
		public Color GridColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.gridColor.Color;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.gridColor = new Pen(value);
			}
		}
		public float HeaderPctHeight
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.headerPctHeight;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.headerPctHeight = value;
			}
		}
		[Browsable(true)]
		public bool IsScrollable
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isScrollable;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isScrollable = value;
				this.vScrollbar.Value = 0;
				this.vScrollbar.Maximum = 0;
				this.vScrollbar.Visible = value;
			}
		}
		[Browsable(true)]
		public bool IsDrawFullRow
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isDrawFullRow;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isDrawFullRow = value;
			}
		}
		[Browsable(true)]
		public bool IsDrawGrid
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isDrawGrid;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isDrawGrid = value;
			}
		}
		[Description("0 = ไม่สามารถวากพื้นหลังได้เลย\r\n1 = วาดสีเดียวตาม focusIndex\r\n2 = วาดเหมือน 1 แต่ control ต้อง focused\r\n3 = วาด 2 สี แยกแบบถ้า focused หรือไม่ focused\r\n4 = วาดแบบตัวหนา")]
		public int RowSelectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.rowSelectType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.rowSelectType = value;
			}
		}
		public Color RowSelectColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.rowSelectColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.rowSelectColor = value;
				this.rowSelectBrush = new SolidBrush(value);
			}
		}
		[Browsable(true)]
		public bool IsDrawHeader
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isDrawHeader;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isDrawHeader = value;
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<ColumnItem> Columns
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._columns;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._columns = value;
			}
		}
		public override Font Font
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return base.Font;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (base.Font != value)
				{
					base.Font = value;
				}
				this.SetTextHightValue();
			}
		}
		public int RowsVisible
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._rowsVisible;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this._rowCount > 0 & value >= 0 & value <= this._rowCount)
				{
					this._rowsVisible = value;
				}
				else
				{
					if (value >= 0)
					{
						this._rowsVisible = value;
					}
				}
				if (this.isScrollable)
				{
					this.vScrollbar.Maximum = this._rowsVisible;
				}
			}
		}
		public float RowHeight
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._rowHeight;
			}
		}
		public int Rows
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._rowCount;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (value >= 0)
				{
					this._rowCount = value;
					this.RowsVisible = value;
					try
					{
						this.AdjustTable();
					}
					catch
					{
					}
				}
			}
		}
		public bool CanBlink
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.canBlink;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.canBlink = value;
				if (this.canBlink)
				{
					this.tmBlink = new Timer();
					this.tmBlink.Interval = 400;
					this.tmBlink.Tick -= new EventHandler(this.tmBlink_Tick);
					this.tmBlink.Tick += new EventHandler(this.tmBlink_Tick);
					this.tmBlink.Start();
					return;
				}
				if (this.tmBlink != null)
				{
					this.tmBlink.Stop();
				}
			}
		}
		public string SortColumnName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sortColumeName;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.ColumnExist(value) || value == string.Empty)
				{
					this.sortColumeName = value;
				}
			}
		}
		public Color ScrollChennelColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.vScrollbar.ChannelColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.vScrollbar.ChannelColor = value;
			}
		}
		public string MainColumn
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.mainColumn;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.ColumnExist(value))
				{
					this.mainColumn = value;
				}
			}
		}
		public int FocusItemIndex
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.focusItemIndex;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.focusItemIndex = value;
			}
		}
		public SortType SortType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sortType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.sortType = value;
			}
		}
		public bool IsAutoRepaint
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isAutoRepaint;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isAutoRepaint = value;
			}
		}
		public bool CanDrag
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.canDrag;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.canDrag = value;
			}
		}
		public bool CanGetMouseMove
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.canGetMouseMove;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.canGetMouseMove = value;
			}
		}
		public int CurrentScroll
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.isScrollable)
				{
					return this._currentScroll;
				}
				return 0;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				try
				{
					if (this.vScrollbar != null && value <= this.vScrollbar.Maximum)
					{
						if (this.vScrollbar.Value != value)
						{
							this.vScrollbar.Value = value;
						}
						if (this._currentScroll != value)
						{
							this._currentScroll = value;
							this.setStartEndforDrawing();
						}
					}
				}
				catch (Exception ex)
				{
					if (!base.DesignMode)
					{
						throw ex;
					}
				}
			}
		}
		public int EndIndexDrawing
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._endIndexDrawing;
			}
		}
		public bool IsControlFocus
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isControlFocus;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SortGrid()
		{
			this.InitializeComponent();
			this.stringFormat = new StringFormat();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			base.UpdateStyles();
			this._records = new List<RecordItem>();
			this._columns = new List<ColumnItem>();
			this.SetTextHightValue();
			if (!base.DesignMode)
			{
				this.timerDragdrop = new Timer();
				this.timerDragdrop.Interval = 30;
				this.timerDragdrop.Tick += new EventHandler(this.timerDragdrop_Tick);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			this.vScrollbar = new Scrollbar();
			base.SuspendLayout();
			this.vScrollbar.ChannelColor = Color.LightGray;
			this.vScrollbar.Cursor = Cursors.Hand;
			this.vScrollbar.Dock = DockStyle.Right;
			this.vScrollbar.HeaderHeight = 0f;
			this.vScrollbar.LargeChange = 10;
			this.vScrollbar.Location = new Point(140, 0);
			this.vScrollbar.Maximum = 100;
			this.vScrollbar.MinimumSize = new Size(8, 0);
			this.vScrollbar.Name = "vScrollbar";
			this.vScrollbar.Size = new Size(10, 150);
			this.vScrollbar.TabIndex = 0;
			this.vScrollbar.Value = 0;
			this.vScrollbar.Visible = false;
			this.vScrollbar.ValueChanged += new EventHandler(this.vScrollBar_ValueChanged);
			this.vScrollbar.KeyUp += new KeyEventHandler(this.vScrollBar_KeyUp);
			this.vScrollbar.KeyDown += new KeyEventHandler(this.vScrollBar_KeyDown);
			this.AllowDrop = true;
			this.BackColor = Color.Black;
			base.Controls.Add(this.vScrollbar);
			this.ForeColor = Color.Black;
			base.Name = "SortGrid";
			base.ResumeLayout(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AdjustTable()
		{
			try
			{
				this._records.Clear();
				for (int i = 1; i <= this._rowCount; i++)
				{
					this._records.Add(new RecordItem(ref this._columns, this.BackColor));
				}
				this.focusItemIndex = -1;
				if (this.isScrollable)
				{
					this.vScrollbar.Maximum = this._rowsVisible;
					this.SetLargeChangeAndEnable();
					this.vScrollbar.Value = 0;
				}
				this.setStartEndforDrawing();
			}
			catch
			{
			}
			finally
			{
				if (!base.DesignMode && this.IsAutoRepaint)
				{
					this.Redraw();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnLayout(LayoutEventArgs e)
		{
			try
			{
				if (this.isScrollable)
				{
					this.SetLargeChangeAndEnable();
					if (this.CurrentScroll > 0)
					{
						this.CurrentScroll = 0;
					}
				}
				this.setStartEndforDrawing();
			}
			catch
			{
			}
			base.OnLayout(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int GetRecordPerPage()
		{
			int result = 0;
			try
			{
				if (this._rowHeight > 0f)
				{
					float num = (float)base.Height;
					if (this.isDrawHeader)
					{
						num -= this._columnHeight;
					}
					result = (int)Math.Floor((double)(num / this._rowHeight));
				}
			}
			catch
			{
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetLargeChangeAndEnable()
		{
			try
			{
				if (this.isScrollable)
				{
					int recordPerPage = this.GetRecordPerPage();
					if (recordPerPage > -1)
					{
						this.vScrollbar.LargeChange = recordPerPage;
						if (this.isScrollable)
						{
							this.vScrollbar.Enabled = true;
						}
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void vScrollBar_ValueChanged(object sender, EventArgs e)
		{
			if (this.isScrollable)
			{
				this.CurrentScroll = this.vScrollbar.Value;
				if (this.isAutoRepaint && !this.isPainting)
				{
					base.Invalidate(false);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ColumnExist(string ColName)
		{
			bool result = false;
			try
			{
				if (this._columns != null)
				{
					foreach (ColumnItem current in this._columns)
					{
						if (current.Name == ColName)
						{
							result = true;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DrawGrid(Graphics g)
		{
			try
			{
				if (this.isDrawGrid)
				{
					int num = 1;
					for (int i = 1; i <= this._rowsVisible; i++)
					{
						float num2 = (float)(num - this._currentScroll) * this._rowHeight + this._columnHeight;
						if (num2 >= this._beginY && num2 < (float)base.Height)
						{
							g.DrawLine(this.gridColor, new PointF(0f, num2), new PointF((float)base.Width, num2));
						}
						else
						{
							if (num2 > (float)base.Height)
							{
								break;
							}
						}
						num++;
					}
				}
				float num3 = 0f;
				int num4 = base.Width;
				if (this.isScrollable)
				{
					num4 -= this.vScrollbar.Width;
				}
				float num5 = 0f;
				if (this.isDrawHeader)
				{
					num5 = this._columnHeight - 1f;
				}
				if (this._rowsVisible > 0 && this.isDrawGrid && !this.isDrawFullRow)
				{
					num5 += this._rowHeight * (float)(this._rowsVisible - this._currentScroll);
				}
				int count = this._columns.Count;
				int num6 = 0;
				foreach (ColumnItem current in this._columns)
				{
					float num7 = (float)Math.Round((double)((float)(current.Width * num4) / 100f), 1);
					current.dW = num7;
					current.dX = num3;
					if (current.Visible)
					{
						if (num6 + 1 < count)
						{
							g.DrawLine(this.gridColor, new PointF(num3 + num7 - 0.5f, 0f), new PointF(num3 + num7 - 0.5f, num5));
						}
						num3 += num7;
					}
					num6++;
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Redraw()
		{
			this.isAutoRepaint = true;
			base.Invalidate(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DrawHeader()
		{
			this.DrawHeader(base.CreateGraphics(), true);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected void DrawHeader(Graphics g, bool isForce)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			try
			{
				if (this.isAutoRepaint || isForce)
				{
					int num4 = base.Width;
					if (this.isScrollable)
					{
						num4 -= this.vScrollbar.Width;
					}
					float emSize = this.Font.Size;
					float num5 = 2f;
					float num6 = 0f;
					if (this.headerPctHeight < 100f)
					{
						emSize = this.Font.Size * (this.headerPctHeight / 100f) + 1.25f;
						num5 = 0f;
					}
					foreach (ColumnItem current in this._columns)
					{
						num3 = (float)(current.Width * num4) / 100f;
						if (current.Visible)
						{
							using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new RectangleF(num, num2, num3, this._columnHeight), current.BackColor, Color.FromArgb(80, current.BackColor), LinearGradientMode.Vertical))
							{
								g.FillRectangle(linearGradientBrush, num, num2, num3, this._columnHeight);
							}
							switch (current.ColumnAlignment)
							{
							case StringAlignment.Near:
								this.stringFormat.Alignment = StringAlignment.Near;
								num6 = 1f;
								break;
							case StringAlignment.Center:
								this.stringFormat.Alignment = StringAlignment.Center;
								num6 = num3 / 2f;
								break;
							}
							using (SolidBrush solidBrush = new SolidBrush(current.FontColor))
							{
								if (current.Name == this.sortColumeName)
								{
									g.DrawString(current.Text, new Font(this.Font.Name, emSize, this.Font.Style | current.MyStyle | FontStyle.Underline | FontStyle.Italic), solidBrush, num + num6, num2 + num5, this.stringFormat);
								}
								else
								{
									if (current.ValueFormat == FormatType.CheckBox && current.Text == string.Empty)
									{
										g.DrawString("o", new Font("Wingdings", emSize, this.Font.Style), solidBrush, num + num6, num2 + num5, this.stringFormat);
									}
									else
									{
										if (current.ValueFormat == FormatType.UpDownSymbol)
										{
											g.DrawString("", new Font("Wingdings", emSize, this.Font.Style), solidBrush, num + num6, num2 + num5, this.stringFormat);
										}
										else
										{
											g.DrawString(current.Text, new Font(this.Font.Name, emSize, this.Font.Style | current.MyStyle), solidBrush, num + num6, num2 + num5, this.stringFormat);
										}
									}
								}
							}
							num += num3;
						}
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DrawRecord(Graphics g, int recordNo, bool isRecordChanged)
		{
			try
			{
				float num = (float)(recordNo - 1 - this._currentScroll) * this._rowHeight;
				if (this.isDrawHeader)
				{
					num += this._columnHeight;
				}
				if (num >= this._beginY && num < (float)base.Height)
				{
					RecordItem recordItem = this._records[recordNo - 1];
					int num2 = base.Width;
					if (this.isScrollable)
					{
						num2 -= this.vScrollbar.Width;
					}
					Brush brush = recordItem.BackBrush;
					if (recordNo == this.focusItemIndex + 1)
					{
						if (this.rowSelectType == 1)
						{
							brush = this.rowSelectBrush;
						}
						else
						{
							if (this.rowSelectType == 2)
							{
								if (this.isControlFocus)
								{
									brush = this.rowSelectBrush;
								}
							}
							else
							{
								if (this.rowSelectType == 3)
								{
									if (this.isControlFocus)
									{
										brush = this.rowSelectBrush;
									}
									else
									{
										brush = this._brushNoSelect;
									}
								}
							}
						}
					}
					if (this.isDrawFullRow && isRecordChanged)
					{
						g.FillRectangle(brush, 0f, num + 1f, (float)num2, this._rowHeight - 1f);
					}
					string text = string.Empty;
					foreach (ColumnItem current in this._columns)
					{
						if (current.Visible)
						{
							float dW = current.dW;
							float dX = current.dX;
							this._flForDraw = recordItem.Fields(current.Name);
							bool flag = isRecordChanged | this._flForDraw.Changed;
							if (this.isDrawFullRow)
							{
								flag = false;
							}
							if (isRecordChanged || this._flForDraw.Changed)
							{
								if (flag)
								{
									Brush brush2;
									if (this._flForDraw.BackColor != Color.Transparent)
									{
										brush2 = this._flForDraw.BackBrush;
									}
									else
									{
										brush2 = brush;
									}
									if (this.canBlink && this._flForDraw.IsBlink > 0)
									{
										if (this._flForDraw.IsBlink == 2)
										{
											brush2 = Brushes.Green;
										}
										else
										{
											if (this._flForDraw.IsBlink == 3)
											{
												brush2 = Brushes.Red;
											}
											else
											{
												if (this._flForDraw.IsBlink == 4)
												{
													brush2 = Brushes.Magenta;
												}
											}
										}
									}
									if (this.isDrawGrid)
									{
										g.FillRectangle(brush2, dX, num + 1f, dW - 1f, this._rowHeight - 1f);
									}
									else
									{
										g.FillRectangle(brush2, dX, num + 1f, dW, this._rowHeight - 1f);
									}
								}
								if (this._flForDraw.Text != null)
								{
									float num3 = 0f;
									switch (current.Alignment)
									{
									case StringAlignment.Near:
										this.stringFormat.Alignment = StringAlignment.Near;
										num3 = 1f;
										break;
									case StringAlignment.Center:
										this.stringFormat.Alignment = StringAlignment.Center;
										num3 = dW / 2f;
										break;
									case StringAlignment.Far:
										this.stringFormat.Alignment = StringAlignment.Far;
										num3 = dW - 2f;
										break;
									}
									if (current.ValueFormat == FormatType.UpDownSymbol)
									{
										if (this._flForDraw.Text.ToString() == "+")
										{
											g.DrawString("é", new Font("Wingdings", this.Font.Size - 1f, this.Font.Style), Brushes.Lime, dX + num3, num + 2f, this.stringFormat);
										}
										else
										{
											if (this._flForDraw.Text.ToString() == "-")
											{
												g.DrawString("ê", new Font("Wingdings", this.Font.Size - 1f, this.Font.Style), Brushes.Red, dX + num3, num + 2f, this.stringFormat);
											}
										}
									}
									else
									{
										if (current.ValueFormat == FormatType.Bitmap)
										{
											if (this._flForDraw.Text.ToString() == "0")
											{
												g.DrawImage(Resource.delete, dX + (current.dW - (float)Resource.delete.Size.Width) / 2f, num + (this._rowHeight - (float)Resource.delete.Size.Height) / 2f);
											}
											else
											{
												if (this._flForDraw.Text.ToString() == "1")
												{
													g.DrawImage(Resource.delete_90CW, dX + (current.dW - (float)Resource.delete_90CW.Size.Width) / 2f, num + (this._rowHeight - (float)Resource.delete_90CW.Size.Height) / 2f);
												}
												else
												{
													if (this._flForDraw.Text.ToString() == "2")
													{
														if (recordNo == this.focusItemIndex + 1)
														{
															g.DrawImage(Resource.info, dX + (current.dW - (float)Resource.info.Size.Width) / 2f, num + (this._rowHeight - (float)Resource.info.Size.Height) / 2f);
														}
													}
													else
													{
														if (this._flForDraw.Text.ToString() == "3")
														{
															if (recordNo == this.focusItemIndex + 1)
															{
																g.DrawImage(Resource.action_check, dX + (current.dW - (float)Resource.action_check.Size.Width) / 2f, num + (this._rowHeight - (float)Resource.action_check.Size.Height) / 2f);
															}
														}
														else
														{
															if (this._flForDraw.Text.ToString() == "4")
															{
																g.DrawImage(Resource.action_delete, dX + (current.dW - (float)Resource.action_delete.Size.Width) / 2f, num + (this._rowHeight - (float)Resource.action_delete.Size.Height) / 2f);
															}
														}
													}
												}
											}
										}
										else
										{
											if (current.ValueFormat == FormatType.CheckBox)
											{
												text = this._flForDraw.Text.ToString();
												if (text == "1")
												{
													text = "n";
													g.DrawString(text, new Font("Wingdings", this.Font.Size, this.Font.Style), Brushes.Red, dX + num3, num + 2f, this.stringFormat);
												}
												else
												{
													if (text == "0")
													{
														text = "o";
														g.DrawString(text, new Font("Wingdings", this.Font.Size, this.Font.Style), Brushes.White, dX + num3, num + 2f, this.stringFormat);
													}
												}
											}
											else
											{
												text = this._flForDraw.Text.ToString();
												if (current.ValueFormat == FormatType.RecordNumber)
												{
													text = recordNo.ToString();
												}
												Brush brush3 = this._flForDraw.FontBrush;
												if (this.canBlink && this._flForDraw.IsBlink != 0)
												{
													brush3 = Brushes.White;
												}
												g.DrawString(text, new Font(this.Font, this.Font.Style | this._flForDraw.FontStyle), brush3, dX + num3, num + 2f, this.stringFormat);
												if (current.ValueFormat == FormatType.Symbol)
												{
													if (this._flForDraw.Tag.ToString() == "@F")
													{
														num3 = g.MeasureString(text, new Font(this.Font, this.Font.Style | this._flForDraw.FontStyle)).Width;
														text = "«";
														g.DrawString(text, new Font("Wingdings", this.Font.Size + 1f, this.Font.Style), Brushes.Pink, dX + num3 - 1f, num + 2f, this.stringFormat);
													}
													else
													{
														if (!string.IsNullOrEmpty(this._flForDraw.Tag.ToString()))
														{
															num3 = g.MeasureString(text, new Font(this.Font, this.Font.Style | this._flForDraw.FontStyle)).Width;
															using (Font font = new Font(this.Font.Name, this.Font.Size - (float)((double)this.Font.Size * 0.3), FontStyle.Regular))
															{
																g.DrawString(" (" + this._flForDraw.Tag.ToString() + ")", font, Brushes.Orange, dX + num3 - 1f, num + (float)((this.Font.Height - font.Height) / 2) + 2f, this.stringFormat);
															}
														}
													}
												}
											}
										}
									}
								}
								this._flForDraw.Changed = false;
							}
						}
					}
					recordItem.Changed = false;
					recordItem = null;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int GetColumnIndexByName(string columnName)
		{
			int num = 0;
			foreach (ColumnItem current in this._columns)
			{
				if (string.Compare(current.Name, columnName, true) == 0)
				{
					return num;
				}
				num++;
			}
			return num;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetTextHightValue()
		{
			this._rowHeight = (float)((int)Math.Ceiling((double)base.CreateGraphics().MeasureString("X", this.Font).Height)) + 2f;
			this._columnHeight = (float)Math.Ceiling((double)(this._rowHeight * (this.headerPctHeight / 100f)));
			if (this.isDrawHeader)
			{
				this._beginY = this._columnHeight;
				this.vScrollbar.HeaderHeight = this._columnHeight;
				return;
			}
			this._beginY = 0f;
			this.vScrollbar.HeaderHeight = 0f;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int SortNumericAnonymousMethod_asc(RecordItem r1, RecordItem r2)
		{
			int num = 0;
			if (string.IsNullOrEmpty(r1.Fields(this.sortColumeName).Text.ToString()) || string.IsNullOrEmpty(r2.Fields(this.sortColumeName).Text.ToString()))
			{
				if (string.IsNullOrEmpty(r1.Fields(this.sortColumeName).Text.ToString()) && !string.IsNullOrEmpty(r2.Fields(this.sortColumeName).Text.ToString()))
				{
					num = 1;
					r2.Changed = true;
				}
				else
				{
					if (!string.IsNullOrEmpty(r1.Fields(this.sortColumeName).Text.ToString()) && string.IsNullOrEmpty(r2.Fields(this.sortColumeName).Text.ToString()))
					{
						num = -1;
					}
					else
					{
						if (this.mainColumn != string.Empty)
						{
							string text = r1.Fields(this.mainColumn).Text.ToString();
							string text2 = r2.Fields(this.mainColumn).Text.ToString();
							if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
							{
								num = 1;
								r2.Changed = true;
							}
							else
							{
								if (!string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
								{
									num = -1;
								}
								else
								{
									num = text.CompareTo(text2);
									r2.Changed = true;
								}
							}
						}
					}
				}
			}
			else
			{
				decimal d = 0m;
				decimal d2 = 0m;
				decimal.TryParse(r1.Fields(this.sortColumeName).Text.ToString(), out d);
				decimal.TryParse(r2.Fields(this.sortColumeName).Text.ToString(), out d2);
				num = decimal.Compare(d, d2);
				if (num > 0)
				{
					r1.Changed = true;
					r2.Changed = true;
				}
			}
			return num;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int SortStringAnonymousMethod_asc(RecordItem r1, RecordItem r2)
		{
			int num = 0;
			if (string.IsNullOrEmpty(r1.Fields(this.sortColumeName).Text.ToString()) || string.IsNullOrEmpty(r2.Fields(this.sortColumeName).Text.ToString()))
			{
				if (string.IsNullOrEmpty(r1.Fields(this.sortColumeName).Text.ToString()) && !string.IsNullOrEmpty(r2.Fields(this.sortColumeName).Text.ToString()))
				{
					num = 1;
					r2.Changed = true;
				}
				else
				{
					if (!string.IsNullOrEmpty(r1.Fields(this.sortColumeName).Text.ToString()) && string.IsNullOrEmpty(r2.Fields(this.sortColumeName).Text.ToString()))
					{
						num = -1;
					}
					else
					{
						if (this.mainColumn != string.Empty)
						{
							string text = r1.Fields(this.mainColumn).Text.ToString();
							string text2 = r2.Fields(this.mainColumn).Text.ToString();
							if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
							{
								num = 1;
								r2.Changed = true;
							}
							else
							{
								if (!string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
								{
									num = -1;
								}
								else
								{
									num = text.CompareTo(text2);
									r2.Changed = true;
								}
							}
						}
					}
				}
			}
			else
			{
				num = string.Compare(r1.Fields(this.sortColumeName).Text.ToString(), r2.Fields(this.sortColumeName).Text.ToString());
				if (num > 0)
				{
					r1.Changed = true;
					r2.Changed = true;
				}
			}
			return num;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int SortNumericAnonymousMethod_desc(RecordItem r1, RecordItem r2)
		{
			int num = 0;
			string text = r1.Fields(this.sortColumeName).Text.ToString();
			string text2 = r2.Fields(this.sortColumeName).Text.ToString();
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
				{
					num = 1;
					r2.Changed = true;
				}
				else
				{
					if (!string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
					{
						num = -1;
					}
					else
					{
						if (this.mainColumn != string.Empty)
						{
							string text3 = r1.Fields(this.mainColumn).Text.ToString();
							string text4 = r2.Fields(this.mainColumn).Text.ToString();
							if (string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4))
							{
								num = 1;
								r2.Changed = true;
							}
							else
							{
								if (!string.IsNullOrEmpty(text3) && string.IsNullOrEmpty(text4))
								{
									num = -1;
								}
								else
								{
									num = text3.CompareTo(text4);
									r2.Changed = true;
								}
							}
						}
					}
				}
			}
			else
			{
				decimal d = 0m;
				decimal d2 = 0m;
				decimal.TryParse(text, out d);
				decimal.TryParse(text2, out d2);
				num = decimal.Compare(d, d2);
				if (num > 0)
				{
					num = -1;
				}
				else
				{
					if (num < 0)
					{
						num = 1;
						r1.Changed = true;
						r2.Changed = true;
					}
				}
			}
			return num;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int SortStringAnonymousMethod_desc(RecordItem r1, RecordItem r2)
		{
			int num = 0;
			if (string.IsNullOrEmpty(r1.Fields(this.sortColumeName).Text.ToString()) || string.IsNullOrEmpty(r2.Fields(this.sortColumeName).Text.ToString()))
			{
				if (string.IsNullOrEmpty(r1.Fields(this.sortColumeName).Text.ToString()) && !string.IsNullOrEmpty(r2.Fields(this.sortColumeName).Text.ToString()))
				{
					num = 1;
					r2.Changed = true;
				}
				else
				{
					if (!string.IsNullOrEmpty(r1.Fields(this.sortColumeName).Text.ToString()) && string.IsNullOrEmpty(r2.Fields(this.sortColumeName).Text.ToString()))
					{
						num = -1;
					}
					else
					{
						if (this.mainColumn != string.Empty)
						{
							string text = r1.Fields(this.mainColumn).Text.ToString();
							string text2 = r2.Fields(this.mainColumn).Text.ToString();
							if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
							{
								num = 1;
								r2.Changed = true;
							}
							else
							{
								if (!string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
								{
									num = -1;
								}
								else
								{
									num = text.CompareTo(text2);
									r2.Changed = true;
								}
							}
						}
					}
				}
			}
			else
			{
				num = string.Compare(r1.Fields(this.sortColumeName).Text.ToString(), r2.Fields(this.sortColumeName).Text.ToString());
				if (num > 0)
				{
					num = -1;
				}
				else
				{
					if (num < 0)
					{
						num = 1;
						r1.Changed = true;
						r2.Changed = true;
					}
				}
			}
			return num;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private TableMouseEventArgs GetTableMouseArgument(MouseEventArgs e)
		{
			TableMouseEventArgs result = null;
			try
			{
				float num = (float)e.Y;
				if (this.IsDrawHeader)
				{
					num -= this._columnHeight;
				}
				int num2 = -1;
				if (num > 0f)
				{
					num2 = Convert.ToInt32(Math.Floor((double)(num / this._rowHeight)));
					num2++;
				}
				int num3 = num2;
				if (num3 > 0)
				{
					num3 += this.CurrentScroll;
				}
				if (num3 <= this._rowsVisible)
				{
					ColumnItem columnItem = null;
					Rectangle fieldPosition = default(Rectangle);
					int num4 = 0;
					int num5 = base.Width;
					if (this.isScrollable)
					{
						num5 -= this.vScrollbar.Width;
					}
					float num6 = (float)(num2 - 1) * this._rowHeight;
					if (this.isDrawHeader)
					{
						num6 += this._columnHeight;
					}
					foreach (ColumnItem current in this._columns)
					{
						if (current.Visible)
						{
							if (e.X >= num4 & (double)e.X <= (double)num4 + (double)(current.Width * num5) / 100.0)
							{
								fieldPosition = new Rectangle(num4, Convert.ToInt32(num6), Convert.ToInt32((double)(current.Width * num5) / 100.0), Convert.ToInt32(this._rowHeight));
								columnItem = current;
								break;
							}
							num4 += Convert.ToInt32((double)(current.Width * num5) / 100.0);
						}
					}
					if (columnItem != null)
					{
						if (num3 > -1)
						{
							num3--;
						}
						result = new TableMouseEventArgs(fieldPosition, e, num3, columnItem);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BeginUpdate()
		{
			this.isAutoRepaint = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmBlink_Tick(object sender, EventArgs e)
		{
			try
			{
				if (this.canBlink && this.isAutoRepaint && this._rowCount > 0)
				{
					using (Graphics graphics = base.CreateGraphics())
					{
						bool flag = false;
						for (int i = 1; i <= this._rowsVisible; i++)
						{
							flag = false;
							foreach (ColumnItem current in this._columns)
							{
								FieldItem fieldItem = this._records[i - 1].Fields(current.Name);
								if (fieldItem.IsBlink != 0 && fieldItem.LastBlink.AddMilliseconds(1000.0) <= DateTime.Now)
								{
									fieldItem.IsBlink = 0;
									fieldItem.Changed = true;
									flag = true;
								}
							}
							if (flag)
							{
								this.DrawRecord(graphics, i, false);
							}
						}
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void EndUpdate()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.EndUpdate));
				return;
			}
			try
			{
				this.isPainting = true;
				using (Graphics graphics = base.CreateGraphics())
				{
					for (int i = this._startIndexDrawing; i <= this._endIndexDrawing; i++)
					{
						this.DrawRecord(graphics, i, this._records[i - 1].Changed);
					}
				}
				this.isPainting = false;
				this.isAutoRepaint = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void setStartEndforDrawing()
		{
			if (base.Height > 0)
			{
				int num = 1;
				if (this._currentScroll > 0)
				{
					num += this._currentScroll;
				}
				this._startIndexDrawing = num;
				if (this._rowHeight > 0f)
				{
					int num2 = (int)((float)base.Height / this._rowHeight) + (((float)base.Height % this._rowHeight > 0f) ? 1 : 0);
					if (this.isDrawHeader)
					{
						num2--;
					}
					int num3 = num + num2 - 1;
					if (num3 > this._rowsVisible)
					{
						num3 = this._rowsVisible;
					}
					this._endIndexDrawing = num3;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void EndUpdate(int recordIndex)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new SortGrid.EndUpdateCallBack(this.EndUpdate), new object[]
				{
					recordIndex
				});
				return;
			}
			try
			{
				if (recordIndex > -1 && recordIndex < this._rowsVisible)
				{
					using (Graphics graphics = base.CreateGraphics())
					{
						this.DrawRecord(graphics, recordIndex + 1, this._records[recordIndex].Changed);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int SortColumeBySize(ColumnItem r1, ColumnItem r2)
		{
			if (r1.Width == r2.Width)
			{
				return 0;
			}
			if (r1.Width < r2.Width)
			{
				return 1;
			}
			return -1;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ColumnItem GetColumn(string columnName)
		{
			return this._columns[this.GetColumnIndexByName(columnName)];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ClearAllText()
		{
			try
			{
				this.focusItemIndex = -1;
				if (this._records != null && this._records.Count > 0)
				{
					foreach (RecordItem current in this._records)
					{
						foreach (ColumnItem current2 in this._columns)
						{
							if (current2.ValueFormat != FormatType.Label)
							{
								FieldItem fieldItem = current.Fields(current2.Name);
								fieldItem.Text = string.Empty;
								if (current2.ValueFormat != FormatType.RecordNumber)
								{
									fieldItem.FontColor = Color.White;
								}
								fieldItem.BackColor = Color.Transparent;
								fieldItem.Tag = string.Empty;
								fieldItem.TempText = string.Empty;
								fieldItem.Changed = true;
								fieldItem.IsBlink = 0;
								fieldItem.FontStyle = FontStyle.Regular;
							}
						}
						current.Changed = true;
					}
				}
				if (this.isScrollable)
				{
					this.vScrollbar.Value = 0;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int GetHeightByRows()
		{
			return this.GetHeightByRows(this._rowsVisible);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int GetHeightByRows(int recordInPage)
		{
			if (this.isDrawHeader)
			{
				return (int)(this._rowHeight * (float)recordInPage + this._columnHeight + 2f);
			}
			return (int)(this._rowHeight * (float)recordInPage + 2f);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetRowByHeight()
		{
			try
			{
				this.Rows = this.GetRecordPerPage();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RecordItem Records(int index)
		{
			RecordItem result;
			try
			{
				result = this._records[index];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RecordItem AddRecord(int position, bool isFixSize)
		{
			RecordItem result;
			try
			{
				RecordItem recordItem = new RecordItem(ref this._columns, this.BackColor);
				if (position == -1)
				{
					this._records.Add(recordItem);
				}
				else
				{
					if (position <= this._records.Count)
					{
						this._records.Insert(position - 1, recordItem);
					}
					else
					{
						this._records.Add(recordItem);
					}
				}
				if (isFixSize)
				{
					if (this._records.Count > this._rowCount)
					{
						this._records.RemoveAt(this._rowCount);
					}
				}
				else
				{
					this._rowCount = this._records.Count;
					this._rowsVisible++;
					if (this.isScrollable)
					{
						this.vScrollbar.Maximum = this._rowCount;
					}
					this.setStartEndforDrawing();
				}
				if (position == -1)
				{
					position = this._rowCount;
				}
				for (int i = position - 1; i < this._rowCount; i++)
				{
					this._records[i].Changed = true;
				}
				if (this.isScrollable)
				{
					if (!isFixSize && this.FocusItemIndex > -1)
					{
						this.SetFocusItem(this.FocusItemIndex + 1);
					}
					this.SetLargeChangeAndEnable();
				}
				result = recordItem;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DeleteRecord(int position)
		{
			try
			{
				if (position + 1 <= this._rowCount)
				{
					this._records.RemoveAt(position);
					this._rowCount--;
					this.focusItemIndex = -1;
					this._rowsVisible--;
					this.setStartEndforDrawing();
					if (this.isScrollable)
					{
						this.SetLargeChangeAndEnable();
						if (this.vScrollbar.Maximum > 0)
						{
							this.vScrollbar.Maximum--;
						}
					}
					base.Invalidate(false);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ClearAllTextByRow(int rowIndex)
		{
			try
			{
				this._records[rowIndex].Changed = true;
				foreach (ColumnItem current in this._columns)
				{
					if (current.ValueFormat != FormatType.Label)
					{
						this._records[rowIndex].Fields(current.Name).Text = string.Empty;
						this._records[rowIndex].Fields(current.Name).IsBlink = 0;
						this._records[rowIndex].Fields(current.Name).Tag = string.Empty;
						this._records[rowIndex].Fields(current.Name).FontStyle = FontStyle.Regular;
						this._records[rowIndex].Fields(current.Name).TempText = string.Empty;
						if (current.ValueFormat != FormatType.RecordNumber)
						{
							this._records[rowIndex].Fields(current.Name).FontColor = Color.White;
						}
						this._records[rowIndex].Fields(current.Name).BackColor = Color.Transparent;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int FindIndex(string columnName, string keyValue)
		{
			int result;
			try
			{
				RecordItem recordItem = this._records.Find((RecordItem item) => item.Fields(columnName).Text.ToString() == keyValue);
				if (recordItem != null)
				{
					result = this._records.IndexOf(recordItem);
				}
				else
				{
					result = -1;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RecordItem Find(string columnName, string keyValue)
		{
			return this._records.Find((RecordItem item) => item.Fields(columnName).Text.ToString() == keyValue);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Sort()
		{
			try
			{
				if (this.sortColumeName != string.Empty)
				{
					switch (this.GetColumn(this.sortColumeName).ValueFormat)
					{
					case FormatType.Price:
					case FormatType.Volume:
					case FormatType.ChangePrice:
					case FormatType.PercentChange:
						if (this.sortType == SortType.Desc)
						{
							this._records.Sort(new Comparison<RecordItem>(this.SortNumericAnonymousMethod_desc));
						}
						else
						{
							this._records.Sort(new Comparison<RecordItem>(this.SortNumericAnonymousMethod_asc));
						}
						break;
					default:
						if (this.sortType == SortType.Desc)
						{
							this._records.Sort(new Comparison<RecordItem>(this.SortStringAnonymousMethod_desc));
						}
						else
						{
							this._records.Sort(new Comparison<RecordItem>(this.SortStringAnonymousMethod_asc));
						}
						break;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Sort(string colName, SortType sortBy)
		{
			try
			{
				if (this.ColumnExist(colName))
				{
					this.sortColumeName = colName;
					this.sortType = sortBy;
					this.Sort();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnFontChanged(EventArgs e)
		{
			this.SetTextHightValue();
			base.OnFontChanged(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnPaint(PaintEventArgs e)
		{
			try
			{
				if (this.isAutoRepaint && !this.isPainting)
				{
					this.isPainting = true;
					if (this.isDrawHeader)
					{
						this.DrawHeader(e.Graphics, false);
					}
					this.DrawGrid(e.Graphics);
					for (int i = this._startIndexDrawing; i <= this._endIndexDrawing; i++)
					{
						this.DrawRecord(e.Graphics, i, true);
					}
				}
			}
			catch
			{
			}
			finally
			{
				this.isPainting = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (!this.isScrollable && this.rowSelectType > 0)
			{
				Keys keyCode = e.KeyCode;
				if (keyCode == Keys.Return)
				{
					e.SuppressKeyPress = true;
					return;
				}
				switch (keyCode)
				{
				case Keys.Up:
					if (this._TableKeyDown != null)
					{
						this._TableKeyDown(e);
					}
					this.SetFocusItem(this.FocusItemIndex - 1);
					e.SuppressKeyPress = true;
					return;
				case Keys.Down:
					if (this._TableKeyDown != null)
					{
						this._TableKeyDown(e);
					}
					this.SetFocusItem(this.FocusItemIndex + 1);
					e.SuppressKeyPress = true;
					return;
				}
				if (this._TableKeyDown != null)
				{
					this._TableKeyDown(e);
				}
				e.SuppressKeyPress = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			if (!this.isScrollable)
			{
				e.IsInputKey = true;
			}
			base.OnPreviewKeyDown(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (this.isScrollable && this.rowSelectType > 1)
			{
				if (e.Delta > 0)
				{
					this.SetFocusItem(this.FocusItemIndex - 1);
					return;
				}
				this.SetFocusItem(this.FocusItemIndex + 1);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			try
			{
				if (e.Button == MouseButtons.Left && e.Clicks == 2)
				{
					TableMouseEventArgs tableMouseArgument = this.GetTableMouseArgument(e);
					if (tableMouseArgument != null)
					{
						if (this.rowSelectType > 1)
						{
							this.SetFocusItem(tableMouseArgument.RowIndex);
						}
						if (this._TableMouseDoubleClick != null)
						{
							this._TableMouseDoubleClick(this, tableMouseArgument);
						}
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			try
			{
				if (!this._hScroll)
				{
					if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && e.Clicks == 1)
					{
						TableMouseEventArgs tableMouseArgument = this.GetTableMouseArgument(e);
						if (tableMouseArgument != null)
						{
							if (tableMouseArgument.RowIndex >= 0 && (tableMouseArgument.Column.ValueFormat == FormatType.CheckBox || tableMouseArgument.Column.ValueFormat == FormatType.Bitmap))
							{
								if (this._records[tableMouseArgument.RowIndex].Fields(tableMouseArgument.Column.Name).Text.ToString() == "1")
								{
									this._records[tableMouseArgument.RowIndex].Fields(tableMouseArgument.Column.Name).Text = "0";
									this._records[tableMouseArgument.RowIndex].Changed = true;
									this.EndUpdate(tableMouseArgument.RowIndex);
								}
								else
								{
									if (this._records[tableMouseArgument.RowIndex].Fields(tableMouseArgument.Column.Name).Text.ToString() == "0")
									{
										this._records[tableMouseArgument.RowIndex].Fields(tableMouseArgument.Column.Name).Text = "1";
										this._records[tableMouseArgument.RowIndex].Changed = true;
										this.EndUpdate(tableMouseArgument.RowIndex);
									}
								}
							}
							if (this.rowSelectType > 1)
							{
								this.SetFocusItem(tableMouseArgument.RowIndex);
							}
							if (this._TableMouseClick != null)
							{
								this._TableMouseClick(this, tableMouseArgument);
							}
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
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.isMouseDown = true;
			this.isFirstMouseMove = true;
			this.mousePoint.X = e.X;
			this.mousePoint.Y = e.Y;
			this._sY = e.Y;
			this._hScroll = false;
			base.OnMouseDown(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			try
			{
				if (this.isMouseDown)
				{
					if (this.CanDrag && this.isFirstMouseMove && (this.mousePoint.X != e.X || this.mousePoint.Y != e.Y))
					{
						this.isFirstMouseMove = false;
						if (this.OnFieldItemDrag(e))
						{
							base.DoDragDrop(this.GetDataForDragDrop(), DragDropEffects.Move);
						}
					}
					if ((float)Math.Abs(this._sY - e.Y) >= this._rowHeight)
					{
						int num = (int)((float)(this._sY - e.Y) / this._rowHeight);
						this._hScroll = (num != 0);
						int num2 = this.vScrollbar.Value + num;
						if (num2 >= 0 && num2 + this.vScrollbar.LargeChange <= this.vScrollbar.Maximum)
						{
							this.vScrollbar.Value += num;
						}
						this._sY = e.Y;
					}
				}
				else
				{
					if (this.canGetMouseMove)
					{
						TableMouseEventArgs tableMouseArgument = this.GetTableMouseArgument(e);
						if (tableMouseArgument != null && this._TableMouseMove != null)
						{
							this._TableMouseMove(tableMouseArgument);
						}
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			try
			{
				this.isMouseDown = false;
				this.isFirstMouseMove = false;
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual bool OnFieldItemDrag(MouseEventArgs e)
		{
			try
			{
				FieldItemDragEventArgs fieldItemDragEventArgs = this.CreateFieldItemDragEventArgs(e);
				bool result;
				if (fieldItemDragEventArgs == null || fieldItemDragEventArgs.Item.Text == null || fieldItemDragEventArgs.Item.Text.ToString() == string.Empty)
				{
					result = false;
					return result;
				}
				this.dragItem = fieldItemDragEventArgs.Item;
				this.isDraging = true;
				this.isDroped = false;
				this.dragFlake = new FlakeDlg(this.dragItem.Text.ToString(), this.dragItem.FontColor);
				this.timerDragdrop.Start();
				result = true;
				return result;
			}
			catch
			{
			}
			return false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
		{
			if (this.CanDrag && qcdevent.Action == DragAction.Drop && qcdevent.KeyState == 0)
			{
				this.isDroped = true;
			}
			base.OnQueryContinueDrag(qcdevent);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);
			try
			{
				if (this.CanDrag)
				{
					this.isDraging = true;
					if (!e.Data.GetDataPresent(typeof(DragItemData).ToString()))
					{
						e.Effect = DragDropEffects.None;
					}
					else
					{
						e.Effect = DragDropEffects.Move;
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnDragOver(DragEventArgs e)
		{
			base.OnDragOver(e);
			this.isDraging = true;
			e.Effect = DragDropEffects.Move;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);
			this.isDraging = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			try
			{
				this.isDraging = false;
				if (this._ItemDragDrop != null)
				{
					DragItemData dragItemData = (DragItemData)e.Data.GetData(typeof(DragItemData).ToString());
					Point point = base.PointToClient(new Point(e.X, e.Y));
					string dragValue = string.Empty;
					if (dragItemData.DragText != null)
					{
						dragValue = dragItemData.DragText.ToString();
						this._ItemDragDrop(this, this.GetTableMouseArgument(new MouseEventArgs(MouseButtons.Left, 0, point.X, point.Y, 0)), dragValue);
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnEnter(EventArgs e)
		{
			this.isControlFocus = true;
			if (this.isAutoRepaint && this._rowCount > 0)
			{
				if (this.FocusItemIndex > -1 && this.FocusItemIndex < this._rowCount)
				{
					this._records[this.FocusItemIndex].Changed = true;
				}
				if (this.isScrollable)
				{
					this.vScrollbar.Focus();
				}
			}
			base.OnEnter(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnLeave(EventArgs e)
		{
			this.isControlFocus = false;
			base.Invalidate(false);
			base.OnLeave(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private DragItemData GetDataForDragDrop()
		{
			DragItemData dragItemData = new DragItemData(this);
			try
			{
				if (this.dragItem.Text != null)
				{
					dragItemData.DragText = this.dragItem.Text.ToString();
				}
				else
				{
					dragItemData.DragText = string.Empty;
				}
			}
			catch
			{
			}
			return dragItemData;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private FieldItemDragEventArgs CreateFieldItemDragEventArgs(MouseEventArgs e)
		{
			FieldItemDragEventArgs fieldItemDragEventArgs = null;
			string text = string.Empty;
			try
			{
				int num = Convert.ToInt32(Math.Floor((double)((float)e.Y / this._rowHeight)));
				if (!this.IsDrawHeader)
				{
					num++;
				}
				if (num > 0)
				{
					num += this.CurrentScroll;
				}
				if (num > -1 & num <= this._rowsVisible)
				{
					Rectangle fieldPosition = default(Rectangle);
					int num2 = 0;
					foreach (ColumnItem current in this._columns)
					{
						if (current.Visible)
						{
							if (e.X >= num2 & (double)e.X <= (double)num2 + (double)(current.Width * base.Width) / 100.0)
							{
								int y = Convert.ToInt32((float)(num + 1) * this._rowHeight);
								int width = Convert.ToInt32((double)(current.Width * base.Width) / 100.0);
								int height = Convert.ToInt32(this._rowHeight);
								fieldPosition = new Rectangle(num2, y, width, height);
								text = current.Name;
								FormatType arg_133_0 = current.ValueFormat;
								break;
							}
							num2 += Convert.ToInt32((double)(current.Width * base.Width) / 100.0);
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						num--;
						if (num >= 0)
						{
							FieldItem fieldItem = this.Records(num).Fields(text);
							if (fieldItem.Text != null)
							{
								fieldItemDragEventArgs = new FieldItemDragEventArgs(fieldPosition, e, num, text, fieldItem);
							}
						}
					}
				}
				if (fieldItemDragEventArgs != null)
				{
					this.dragItem = fieldItemDragEventArgs.Item;
				}
				else
				{
					this.dragItem = null;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			text = null;
			return fieldItemDragEventArgs;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private FieldItem GetItemAt(Point point)
		{
			return this.GetItemAt(point.X, point.Y);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private FieldItem GetItemAt(int x, int y)
		{
			FieldItem result = null;
			try
			{
				int num = Convert.ToInt32(Math.Floor((double)((float)y / this._rowHeight)));
				if (!this.IsDrawHeader)
				{
					num++;
				}
				if (num > 0)
				{
					num += this.CurrentScroll;
				}
				if (num >= 0 & num <= this._rowsVisible)
				{
					string text = string.Empty;
					int num2 = 0;
					foreach (ColumnItem current in this._columns)
					{
						if (x >= num2 & (double)x <= (double)num2 + (double)(current.Width * base.Width) / 100.0)
						{
							new Rectangle(num2, Convert.ToInt32((float)num * this._rowHeight), Convert.ToInt32((double)(current.Width * base.Width) / 100.0), Convert.ToInt32(this._rowHeight));
							text = current.Name;
							break;
						}
						num2 += Convert.ToInt32((double)(current.Width * base.Width) / 100.0);
					}
					if (!string.IsNullOrEmpty(text))
					{
						num--;
						if (num >= 0)
						{
							result = this.Records(num).Fields(text);
						}
					}
					text = null;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerDragdrop_Tick(object sender, EventArgs e)
		{
			try
			{
				if (this.dragFlake != null)
				{
					if (this.isDroped)
					{
						this.dragFlake.Close();
						this.dragFlake = null;
						base.Invalidate(true);
						this.timerDragdrop.Stop();
					}
					else
					{
						if (this.isDraging)
						{
							this.dragFlake.Show(Control.MousePosition.X, Control.MousePosition.Y - this.dragFlake.Height);
						}
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void vScrollBar_KeyDown(object sender, KeyEventArgs e)
		{
			if (this._TableKeyDown != null)
			{
				this._TableKeyDown(e);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void vScrollBar_KeyUp(object sender, KeyEventArgs e)
		{
			if (this.isScrollable)
			{
				Keys keyCode = e.KeyCode;
				if (keyCode == Keys.Return)
				{
					e.SuppressKeyPress = true;
					return;
				}
				switch (keyCode)
				{
				case Keys.Prior:
					if (this.CurrentScroll - this.vScrollbar.LargeChange > -1)
					{
						this.CurrentScroll -= this.vScrollbar.LargeChange;
					}
					else
					{
						this.CurrentScroll = 0;
					}
					this.SetFocusItem(this.focusItemIndex - this.vScrollbar.LargeChange);
					e.SuppressKeyPress = true;
					return;
				case Keys.Next:
					if (this.CurrentScroll + this.vScrollbar.LargeChange < this._rowCount)
					{
						int largeChange = this.vScrollbar.LargeChange;
						int num = this._rowsVisible - this._endIndexDrawing + (this._endIndexDrawing - this._startIndexDrawing - largeChange + 1);
						int num2;
						if (num > largeChange)
						{
							num2 = largeChange;
						}
						else
						{
							num2 = num;
						}
						this.CurrentScroll += num2;
						this.SetFocusItem(this.focusItemIndex + largeChange);
					}
					e.SuppressKeyPress = true;
					return;
				case Keys.End:
					e.SuppressKeyPress = true;
					return;
				case Keys.Home:
					e.SuppressKeyPress = true;
					return;
				case Keys.Up:
					this.SetFocusItem(this.FocusItemIndex - 1);
					e.SuppressKeyPress = true;
					return;
				case Keys.Down:
					this.SetFocusItem(this.FocusItemIndex + 1);
					e.SuppressKeyPress = true;
					return;
				}
				if (this._TableKeyDown != null)
				{
					this._TableKeyDown(e);
				}
				e.SuppressKeyPress = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetFocusItem(int newIndex)
		{
			try
			{
				if (newIndex > -1 && newIndex < this._rowsVisible)
				{
					int num = this.FocusItemIndex;
					this.focusItemIndex = newIndex;
					this._records[newIndex].Changed = true;
					if (this.isAutoRepaint)
					{
						this.EndUpdate(newIndex);
					}
					if (num > -1 && newIndex != num)
					{
						this._records[num].Changed = true;
						if (this.isAutoRepaint)
						{
							this.EndUpdate(num);
						}
					}
					if (!this.isDrawHeader)
					{
						if (newIndex > this.CurrentScroll + this.vScrollbar.LargeChange)
						{
							this.CurrentScroll++;
						}
						else
						{
							if (newIndex < this.vScrollbar.Value)
							{
								this.CurrentScroll = newIndex;
							}
						}
					}
					else
					{
						if (newIndex > this.CurrentScroll + this.vScrollbar.LargeChange)
						{
							this.CurrentScroll = newIndex;
						}
						else
						{
							if (newIndex == this.CurrentScroll + this.vScrollbar.LargeChange)
							{
								this.CurrentScroll++;
							}
							else
							{
								if (newIndex < this.CurrentScroll)
								{
									this.CurrentScroll = newIndex;
								}
							}
						}
					}
					if (this.rowSelectType > 1 && this._TableFocusIndexChanged != null)
					{
						this._TableFocusIndexChanged(this, this.focusItemIndex);
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetCenterScroll(int newIndex)
		{
			try
			{
				float num = (float)base.Height;
				if (this.isDrawHeader)
				{
					num -= this._columnHeight;
				}
				int num2 = (int)(num / this._rowHeight) + ((num % this._rowHeight > 0f) ? 1 : 0);
				int num3 = newIndex - (num2 - 1) / 2;
				if (num3 < 0)
				{
					num3 = 0;
				}
				this.CurrentScroll = num3;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ClearFocusItem()
		{
			try
			{
				if (this.FocusItemIndex > -1 && this.FocusItemIndex < this._rowsVisible)
				{
					int num = this.FocusItemIndex;
					this.focusItemIndex = -1;
					this._records[num].Changed = true;
					this.EndUpdate(num);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Rectangle GetPosition(int rowIndex, string columnName)
		{
			Rectangle result = default(Rectangle);
			try
			{
				if (rowIndex > -1 & rowIndex <= this._rowsVisible)
				{
					int num = 0;
					int num2 = base.Width;
					if (this.isScrollable)
					{
						num2 -= this.vScrollbar.Width;
						rowIndex -= this.vScrollbar.Value;
					}
					float num3 = (float)rowIndex * this._rowHeight;
					if (this.isDrawHeader)
					{
						num3 += this._columnHeight;
					}
					foreach (ColumnItem current in this._columns)
					{
						if (current.Visible)
						{
							if (current.Name == columnName)
							{
								result = new Rectangle(num, Convert.ToInt32(num3), Convert.ToInt32((double)(current.Width * base.Width) / 100.0), Convert.ToInt32(this._rowHeight));
								break;
							}
							num += Convert.ToInt32((double)(current.Width * num2) / 100.0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			try
			{
				this._flForDraw = null;
				if (this._columns != null)
				{
					this._columns.Clear();
					this._columns = null;
				}
				if (this.timerDragdrop != null)
				{
					this.timerDragdrop.Stop();
					this.timerDragdrop = null;
				}
				if (this.dragFlake != null)
				{
					this.dragFlake.Dispose();
					this.dragFlake = null;
				}
				this.dragItem = null;
				this.gridColor = null;
				if (this._records != null)
				{
					this._records.Clear();
					this._records = null;
				}
				if (this.gridColor != null)
				{
					this.gridColor.Dispose();
					this.gridColor = null;
				}
				if (this._brushNoSelect != null)
				{
					this._brushNoSelect.Dispose();
					this._brushNoSelect = null;
				}
				if (this.rowSelectBrush != null)
				{
					this.rowSelectBrush.Dispose();
					this.rowSelectBrush = null;
				}
				if (this.stringFormat != null)
				{
					this.stringFormat.Dispose();
					this.stringFormat = null;
				}
				if (this.tmBlink != null)
				{
					this.tmBlink.Enabled = false;
					this.tmBlink = null;
				}
				if (this.vScrollbar != null)
				{
					this.vScrollbar.Dispose();
					this.vScrollbar = null;
				}
			}
			catch
			{
			}
		}
	}
}
