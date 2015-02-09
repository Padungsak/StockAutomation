using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace STIControl.ExpandTableGrid
{
	[Designer(typeof(SpeedTableGridDesigner))]
	public class ExpandGrid : UserControl, IColumnContainer2
	{
		public delegate void ItemDragDropEventHandler(object sender, TableMouseEventArgs e, string dragValue);
		public delegate void TableMouseClickEventHandler(object sender, TableMouseEventArgs e);
		public delegate void TableMouseDoubleClickEventHandler(object sender, TableMouseEventArgs e);
		public delegate void TableKeyDownEventHandler(KeyEventArgs e);
		public delegate void TableHeaderMouseMoveEventHandler(TableMouseEventArgs e);
		public delegate void TableFocusIndexChangedEventHandler(object sender, int Index);
		public delegate void TableClickExpandEventHandler(object sender, TableMouseEventArgs e);
		private delegate void EndUpdateCallBack(int recordIndex);
		public const string STAR_TAG_FIELD = "@F";
		public const string UP_TAG_FIELD = "@+";
		public const string DOWN_TAG_FIELD = "@-";
		public static int ColumnID;
		private int _rowCount;
		private bool isAutoRepaint = true;
		private StringFormat stringFormat;
		private SortType sortType = SortType.Desc;
		private string sortColumeName = string.Empty;
		private string mainColumn = string.Empty;
		private int _startIndex = 1;
		private Pen gridColor = new Pen(Color.FromArgb(45, 45, 45));
		private bool _isMouseDown;
		private bool _isFirstMouseMove;
		private Point mousePoint = default(Point);
		private List<RecordItem> _records;
		private List<ColumnItem> _columns;
		private Timer tmBlink;
		private Scrollbar vScrollbar;
		private Timer timerDragdrop;
		private bool isDraging;
		private FlakeDlg dragFlake;
		private FieldItem dragItem;
		private float headerPctHeight = 100f;
		private bool isScrollable;
		private bool isDrawGrid = true;
		private int rowSelectType;
		private Brush rowSelectBrush = Brushes.CadetBlue;
		private Color rowSelectColor = Color.CadetBlue;
		private bool isDrawHeader = true;
		private float columnHeight;
		private float _rowHeight;
		private bool canBlink = true;
		private int focusItemIndex = -1;
		private bool canDrag;
		private bool isDroped;
		private HScrollbar hScrollbar;
		private bool canGetMouseMove;
		private int _currentScroll;
		private int _startDrawIndex = 1;
		private int _endDrawIndex = 1;
		private float _formWidth;
		private float _totColWidth;
		private Brush _brushNoSelect = new SolidBrush(Color.FromArgb(64, 64, 64));
		private FieldItem _flForDraw;
		private float _beginY;
		private bool isPainting;
		private int _sY;
		private bool _hScroll;
		private bool isControlFocus;
		private bool _hScrollVisible;
        public ExpandGrid.ItemDragDropEventHandler _ItemDragDrop;
		public event ExpandGrid.ItemDragDropEventHandler ItemDragDrop
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._ItemDragDrop += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._ItemDragDrop -= value;
			}
		}
       public ExpandGrid.TableMouseClickEventHandler _TableMouseClick;
		public event ExpandGrid.TableMouseClickEventHandler TableMouseClick
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
        public  ExpandGrid.TableMouseDoubleClickEventHandler _TableMouseDoubleClick;
		public event ExpandGrid.TableMouseDoubleClickEventHandler TableMouseDoubleClick
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._TableMouseDoubleClick +=value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._TableMouseDoubleClick -= value;
			}
		}
        public  ExpandGrid.TableKeyDownEventHandler _TableKeyDown;
		public event ExpandGrid.TableKeyDownEventHandler TableKeyDown
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
        public  ExpandGrid.TableHeaderMouseMoveEventHandler _TableHeaderMouseMove;
		public event ExpandGrid.TableHeaderMouseMoveEventHandler TableHeaderMouseMove
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._TableHeaderMouseMove += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._TableHeaderMouseMove -= value;
			}
		}
        public  ExpandGrid.TableFocusIndexChangedEventHandler _TableFocusIndexChanged;
		public event ExpandGrid.TableFocusIndexChangedEventHandler TableFocusIndexChanged
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
        public  ExpandGrid.TableClickExpandEventHandler _TableClickExpand;
		public event ExpandGrid.TableClickExpandEventHandler TableClickExpand
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._TableClickExpand +=value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._TableClickExpand -= value;
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
				this.vScrollbar.Maximum = 1;
				this.vScrollbar.Visible = value;
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
					this.AdjustTable();
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
				return this._currentScroll;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				try
				{
					if (this.isScrollable && value <= this.vScrollbar.Maximum)
					{
						if (this.vScrollbar.Value != value)
						{
							this.vScrollbar.Value = value;
						}
						if (this._currentScroll != value)
						{
							this._currentScroll = value;
							this.SetStartDrawIndex();
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
		public int StartDrawIndex
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._startDrawIndex;
			}
		}
		public int EndDrawIndex
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._endDrawIndex;
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
				this.hScrollbar.ChannelColor = value;
			}
		}
		public override Color BackColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return base.BackColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				base.BackColor = value;
				if (this._records != null)
				{
					foreach (RecordItem current in this._records)
					{
						current.BackColor = value;
					}
					base.Invalidate();
				}
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
		public ExpandGrid()
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
			this.hScrollbar = new HScrollbar();
			this.vScrollbar = new Scrollbar();
			base.SuspendLayout();
			this.hScrollbar.ChannelColor = Color.LightGray;
			this.hScrollbar.LargeChange = 10;
			this.hScrollbar.Location = new Point(0, 140);
			this.hScrollbar.Maximum = 100;
			this.hScrollbar.MinimumSize = new Size(8, 0);
			this.hScrollbar.Name = "hScrollbar";
			this.hScrollbar.Size = new Size(139, 10);
			this.hScrollbar.TabIndex = 1;
			this.hScrollbar.Value = 0;
			this.hScrollbar.VisibleChanged += new EventHandler(this.hScrollbar_VisibleChanged);
			this.hScrollbar.ValueChanged += new EventHandler(this.hScrollbar_ValueChanged);
			this.vScrollbar.ChannelColor = Color.LightGray;
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
			base.Controls.Add(this.hScrollbar);
			base.Controls.Add(this.vScrollbar);
			this.ForeColor = Color.Black;
			base.Name = "ExpandGrid";
			base.ResumeLayout(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AdjustTable()
		{
			try
			{
				foreach (RecordItem current in this._records)
				{
					current.ClearSubRecord();
				}
				this._records.Clear();
				for (int i = 1; i <= this._rowCount; i++)
				{
					this._records.Add(new RecordItem(ref this._columns, i, this.BackColor));
				}
				this.focusItemIndex = -1;
				if (this.isScrollable)
				{
					this.vScrollbar.Maximum = this._rowCount;
					this.SetLargeChangeAndEnable();
					this.vScrollbar.Value = 0;
				}
				this.CalcColumnWidth();
				this.SetStartDrawIndex();
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
					this.vScrollbar.SetBounds(base.ClientSize.Width - 10, 0, 10, base.ClientSize.Height);
					this.hScrollbar.SetBounds(0, base.ClientSize.Height - 10, base.ClientSize.Width - this.vScrollbar.Width, 10);
					this._formWidth = (float)base.Width;
					if (this.isScrollable)
					{
						this._formWidth -= (float)this.vScrollbar.Width;
					}
					if (this.CurrentScroll > 0)
					{
						this.CurrentScroll = 0;
					}
					else
					{
						this.SetStartDrawIndex();
					}
				}
			}
			catch
			{
			}
			base.OnLayout(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetLargeChangeAndEnable()
		{
			try
			{
				if (this.isScrollable)
				{
					int rowCountPerPage = this.GetRowCountPerPage();
					if (rowCountPerPage > -1)
					{
						this.vScrollbar.LargeChange = rowCountPerPage;
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
		private void SetStartDrawIndex()
		{
			try
			{
				this._startDrawIndex = this._startIndex;
				if (this._currentScroll > 0)
				{
					this._startDrawIndex += this._currentScroll;
				}
				for (int i = 0; i < this._rowCount; i++)
				{
					if (this._startDrawIndex >= this._records[i].Index && this._startDrawIndex <= this._records[i].Index + this._records[i].Rows - 1)
					{
						this._startDrawIndex = i + 1;
						break;
					}
				}
				int num;
				if (this.IsDrawHeader)
				{
					num = (int)(((float)base.Height - this.columnHeight) / this._rowHeight) + ((((float)base.Height - this.columnHeight) % this._rowHeight > 0f) ? 1 : 0);
				}
				else
				{
					num = (int)((float)base.Height / this._rowHeight) + (((float)base.Height % this._rowHeight > 0f) ? 1 : 0);
				}
				this._endDrawIndex = this._startDrawIndex + num - 1;
				if (this._endDrawIndex > this._rowCount)
				{
					this._endDrawIndex = this._rowCount;
				}
				for (int j = this._endDrawIndex; j >= this._startDrawIndex; j--)
				{
					if (this._records[j - 1].Index - this.CurrentScroll <= num)
					{
						this._endDrawIndex = j;
						break;
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
			try
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
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string FormatDisplayText(object objText, FormatType formatType)
		{
			string result = objText.ToString();
			switch (formatType)
			{
			case FormatType.Price:
				result = this.PriceFormat(objText, false, string.Empty);
				break;
			case FormatType.Volume:
				result = this.VolumeFormat(objText, true);
				break;
			case FormatType.ChangePrice:
				result = this.PriceFormat(objText, true, string.Empty);
				break;
			case FormatType.PercentChange:
				result = this.PriceFormat(objText, true, "%");
				break;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string FormatDisplayText(int recordNo, object objText, FormatType formatType)
		{
			string text = string.Empty;
			if (formatType == FormatType.RecordNumber)
			{
				text = recordNo.ToString();
			}
			else
			{
				text = objText.ToString();
			}
			return this.FormatDisplayText(objText, formatType);
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
					int num = this._startIndex + this._rowCount;
					if (num > this._rowCount + 1)
					{
						num = this._rowCount + 1;
					}
					int num2 = 0;
					for (int i = this._startIndex; i < num; i++)
					{
						num2 += this._records[i - 1].Rows;
						float num3 = (float)(num2 - this._currentScroll) * this._rowHeight + this.columnHeight;
						if (num3 >= this._beginY && num3 < (float)base.Height)
						{
							g.DrawLine(this.gridColor, new PointF(0f, num3), new PointF((float)base.Width, num3));
						}
						else
						{
							if (num3 >= (float)base.Height)
							{
								break;
							}
						}
					}
				}
				float num4 = 0f;
				if (this.isDrawHeader)
				{
					num4 = this.columnHeight - 1f;
				}
				if (this._rowCount > 0 && this.isDrawGrid)
				{
					num4 += this._rowHeight * (float)(this._records[this._rowCount - 1].Index + this._records[this._rowCount - 1].Rows - this._currentScroll - 1);
				}
				int arg_163_0 = this._columns.Count;
				int num5 = 0;
				float num6 = 0f;
				int num7 = 1;
				foreach (ColumnItem current in this._columns)
				{
					float num8 = (float)Math.Round((double)((float)current.Width * this._formWidth / 100f), 1);
					current.dW = num8;
					current.dX = num6;
					if (current.Visible)
					{
						if (num6 + num8 + 1f < this._formWidth)
						{
							g.DrawLine(this.gridColor, new PointF(num6 + num8 - 0.5f, 0f), new PointF(num6 + num8 - 0.5f, num4));
						}
						num6 += num8;
						num7++;
						if (num7 == 2 && this._hScrollVisible)
						{
							g.SetClip(new RectangleF(num6, 0f, this._formWidth - num8, num4));
							num6 -= (float)this.hScrollbar.Value * this._formWidth / 100f;
						}
					}
					num5++;
				}
				if (this._hScrollVisible)
				{
					g.ResetClip();
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
		public void CalcColumnWidth()
		{
			int num = base.Width;
			if (this.isScrollable)
			{
				num -= this.vScrollbar.Width;
			}
			this._totColWidth = 0f;
			foreach (ColumnItem current in this._columns)
			{
				if (current.Visible)
				{
					this._totColWidth += (float)current.Width;
				}
			}
			this.hScrollbar.Value = 0;
			if (this._totColWidth > 100f)
			{
				this.hScrollbar.Maximum = (int)this._totColWidth;
				this.hScrollbar.LargeChange = 100;
				this.hScrollbar.Visible = true;
				return;
			}
			this.hScrollbar.Visible = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected void DrawHeader(Graphics g, bool isForce)
		{
			try
			{
				if (this.isAutoRepaint || isForce)
				{
					int num = base.Width;
					if (this.isScrollable)
					{
						num -= this.vScrollbar.Width;
					}
					float num2 = 0f;
					float num3 = 0f;
					float emSize = this.Font.Size;
					float num4 = 2f;
					if (this.headerPctHeight < 100f)
					{
						emSize = this.Font.Size * (this.headerPctHeight / 100f) + 1.25f;
						num4 = 0f;
					}
					float num5 = 0f;
					int num6 = 1;
					foreach (ColumnItem current in this._columns)
					{
						num5 = (float)(current.Width * num) / 100f;
						if (current.Visible)
						{
							using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new RectangleF(num2, num3, num5, this.columnHeight), current.BackColor, Color.FromArgb(80, current.BackColor), LinearGradientMode.Vertical))
							{
								g.FillRectangle(linearGradientBrush, num2, num3, num5, this.columnHeight);
							}
							this.stringFormat.Alignment = StringAlignment.Center;
							float num7 = num5 / 2f;
							using (SolidBrush solidBrush = new SolidBrush(current.FontColor))
							{
								if (current.Name == this.sortColumeName)
								{
									g.DrawString(current.Text, new Font(this.Font.Name, emSize, this.Font.Style | current.MyStyle | FontStyle.Underline | FontStyle.Italic), solidBrush, num2 + num7, num3 + num4, this.stringFormat);
								}
								else
								{
									if (current.ValueFormat == FormatType.CheckBox)
									{
										g.DrawString("o", new Font("Wingdings", emSize, this.Font.Style), solidBrush, num2 + num7, num3 + num4, this.stringFormat);
									}
									else
									{
										if (current.ValueFormat == FormatType.UpDownSymbol)
										{
											g.DrawString("", new Font("Wingdings", emSize, this.Font.Style), solidBrush, num2 + num7, num3 + num4, this.stringFormat);
										}
										else
										{
											g.DrawString(current.Text, new Font(this.Font.Name, emSize, this.Font.Style | current.MyStyle), solidBrush, num2 + num7, num3 + num4, this.stringFormat);
										}
									}
								}
							}
							num2 += num5;
							num6++;
							if (num6 == 2 && this._hScrollVisible)
							{
								g.SetClip(new RectangleF(num2, num3, (float)num - num5, this.columnHeight));
								num2 -= (float)(this.hScrollbar.Value * num / 100);
							}
						}
					}
					if (this._hScrollVisible)
					{
						g.ResetClip();
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ExpandRows(int rowIndex, int newRows, string colExpand, bool isMaximize)
		{
			try
			{
				if (colExpand != string.Empty)
				{
					if (isMaximize)
					{
						this._records[rowIndex].Fields(colExpand).TextFront = "1";
					}
					else
					{
						this._records[rowIndex].Fields(colExpand).TextFront = "0";
					}
				}
				int rows = this._records[rowIndex].Rows;
				if (rows != newRows)
				{
					this._records[rowIndex].Rows = newRows;
					this._records[rowIndex].ClearSubRecord();
					int num = this._records[rowIndex].Index + newRows;
					for (int i = rowIndex + 1; i < this._rowCount; i++)
					{
						RecordItem recordItem = this._records[i];
						recordItem.Index = num;
						num += recordItem.Rows;
					}
					this.SetStartDrawIndex();
					this.vScrollbar.Maximum += newRows - rows;
					base.Invalidate(false);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool DrawRecord(Graphics g, int recordNo, bool isRecordChanged)
		{
			bool result = false;
			try
			{
				RecordItem recordItem = this._records[recordNo - 1];
				float num = (float)(recordItem.Index - this._startIndex - this._currentScroll) * this._rowHeight;
				if (this.isDrawHeader)
				{
					num += this.columnHeight;
				}
				if (num >= this._beginY && num < (float)base.Height)
				{
					float num2 = 0f;
					float num3 = 0f;
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
					int num4 = 1;
					string s = string.Empty;
					foreach (ColumnItem current in this._columns)
					{
						if (current.Visible && current.dX < this._formWidth)
						{
							num3 = current.dW;
							float dX = current.dX;
							this._flForDraw = recordItem.Fields(current.Name);
							if (isRecordChanged || this._flForDraw.Changed)
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
									g.FillRectangle(brush2, dX, num + 1f, num3 - 1f, this._rowHeight - 1f);
								}
								else
								{
									g.FillRectangle(brush2, dX, num + 1f, num3, this._rowHeight - 1f);
								}
								if (this._flForDraw.Text != null)
								{
									num2 = 0f;
									switch (current.Alignment)
									{
									case StringAlignment.Near:
										this.stringFormat.Alignment = StringAlignment.Near;
										num2 = 1f;
										break;
									case StringAlignment.Center:
										this.stringFormat.Alignment = StringAlignment.Center;
										num2 = num3 / 2f;
										break;
									case StringAlignment.Far:
										this.stringFormat.Alignment = StringAlignment.Far;
										num2 = num3 - 2f;
										break;
									}
									if (current.ValueFormat == FormatType.UpDownSymbol)
									{
										if (this._flForDraw.Text.ToString() == "+")
										{
											g.DrawString("é", new Font("Wingdings", this.Font.Size - 1f, this.Font.Style), Brushes.Lime, dX + num2, num + 2f, this.stringFormat);
										}
										else
										{
											if (this._flForDraw.Text.ToString() == "-")
											{
												g.DrawString("ê", new Font("Wingdings", this.Font.Size - 1f, this.Font.Style), Brushes.Red, dX + num2, num + 2f, this.stringFormat);
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
											if (current.ValueFormat == FormatType.PriceAndCompare)
											{
												if (this._flForDraw.Tag.ToString() != string.Empty)
												{
													num2 -= 12.5f;
												}
												this.drawText(g, this._flForDraw.Text.ToString(), this._flForDraw.FontBrush, current.IsExpand, dX, num, ref num2);
												if (this._flForDraw.Tag.ToString() == "@+")
												{
													g.DrawString("é", new Font("Wingdings", this.Font.Size - 1f, this.Font.Style), Brushes.Lime, dX + num3, num + 2f, this.stringFormat);
												}
												else
												{
													if (this._flForDraw.Tag.ToString() == "@-")
													{
														g.DrawString("ê", new Font("Wingdings", this.Font.Size - 1f, this.Font.Style), Brushes.Red, dX + num3, num + 2f, this.stringFormat);
													}
												}
											}
											else
											{
												if (current.ValueFormat == FormatType.Symbol)
												{
													this.drawText(g, this._flForDraw.Text.ToString(), this._flForDraw.FontBrush, current.IsExpand, dX, num, ref num2);
													if (this._flForDraw.Tag.ToString() == "@F")
													{
														num2 += g.MeasureString(this._flForDraw.Text.ToString(), new Font(this.Font, this.Font.Style | this._flForDraw.FontStyle)).Width;
														s = "«";
														g.DrawString(s, new Font("Wingdings", this.Font.Size + 1f, this.Font.Style), Brushes.Pink, dX + num2 - 1f, num + 2f, this.stringFormat);
														goto IL_9A3;
													}
													if (string.IsNullOrEmpty(this._flForDraw.Tag.ToString()))
													{
														goto IL_9A3;
													}
													num2 += g.MeasureString(this._flForDraw.Text.ToString(), new Font(this.Font, this.Font.Style | this._flForDraw.FontStyle)).Width;
													using (Font font = new Font(this.Font.Name, this.Font.Size - (float)((double)this.Font.Size * 0.3), FontStyle.Regular))
													{
														g.DrawString(" (" + this._flForDraw.Tag.ToString() + ")", font, Brushes.Orange, dX + num2 - 1f, num + (float)((this.Font.Height - font.Height) / 2) + 2f, this.stringFormat);
														goto IL_9A3;
													}
												}
												this.drawText(g, this._flForDraw.Text.ToString(), this._flForDraw.FontBrush, current.IsExpand, dX, num, ref num2);
											}
										}
									}
								}
								IL_9A3:
								this._flForDraw.Changed = false;
							}
							num4++;
							if (num4 == 2 && this._hScrollVisible)
							{
								g.SetClip(new RectangleF(num3, num + 1f, this._formWidth, this._rowHeight - 1f));
							}
						}
					}
					if (this._hScrollVisible)
					{
						g.ResetClip();
					}
					recordItem.Changed = false;
					brush = null;
					result = true;
				}
				recordItem = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void drawText(Graphics g, string drawText, Brush fontBrush, bool isExpand, float x, float y, ref float adjustX)
		{
			try
			{
				if (this.canBlink && this._flForDraw.IsBlink != 0)
				{
					fontBrush = Brushes.White;
				}
				if (isExpand && this._flForDraw.Text.ToString() != string.Empty)
				{
					if (this._flForDraw.TextFront.ToString().Equals("1"))
					{
						g.DrawImage(Resource.collapse, new PointF(x + 1f, y + (float)((int)((this._rowHeight - (float)Resource.collapse.Size.Height) / 2f))));
					}
					else
					{
						g.DrawImage(Resource.expand, new PointF(x + 1f, y + (float)((int)((this._rowHeight - (float)Resource.expand.Size.Height) / 2f))));
					}
					adjustX += 10f;
				}
				g.DrawString(drawText, new Font(this.Font, this.Font.Style | this._flForDraw.FontStyle), fontBrush, x + adjustX, y + 2f, this.stringFormat);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DrawSubRecord(Graphics g, int recordNoParent, int subRecordNo, bool isRecordChanged)
		{
			try
			{
				float num = (float)(this._records[recordNoParent - 1].Index + subRecordNo - this._startIndex - this._currentScroll) * this._rowHeight;
				if (this.isDrawHeader)
				{
					num += this.columnHeight;
				}
				if (num >= this._beginY && num < (float)base.Height)
				{
					SubRecordItem subRecordItem = this._records[recordNoParent - 1].SubRecord[subRecordNo - 1];
					float num2 = 0f;
					int num3 = 1;
					string text = string.Empty;
					foreach (ColumnItem current in this._columns)
					{
						if (current.Visible)
						{
							num2 = current.dW;
							float dX = current.dX;
							this._flForDraw = subRecordItem.Fields(current.Name);
							if (isRecordChanged || this._flForDraw.Changed)
							{
								Brush brush = this._records[recordNoParent - 1].BackBrush;
								if (this._flForDraw.BackColor != Color.Transparent)
								{
									brush = this._flForDraw.BackBrush;
								}
								if (this.canBlink)
								{
									if (this._flForDraw.IsBlink == 2)
									{
										brush = Brushes.Green;
									}
									else
									{
										if (this._flForDraw.IsBlink == 3)
										{
											brush = Brushes.Red;
										}
										else
										{
											if (this._flForDraw.IsBlink == 4)
											{
												brush = Brushes.Magenta;
											}
										}
									}
								}
								if (this.isDrawGrid)
								{
									g.FillRectangle(brush, dX, num + 1f, num2 - 1f, this._rowHeight - 1f);
								}
								else
								{
									g.FillRectangle(brush, dX, num + 1f, num2, this._rowHeight - 1f);
								}
								if (this._flForDraw.Text.ToString() != string.Empty)
								{
									float num4 = 0f;
									switch (current.Alignment)
									{
									case StringAlignment.Near:
										this.stringFormat.Alignment = StringAlignment.Near;
										num4 = 1f;
										break;
									case StringAlignment.Center:
										this.stringFormat.Alignment = StringAlignment.Center;
										num4 = num2 / 2f;
										break;
									case StringAlignment.Far:
										this.stringFormat.Alignment = StringAlignment.Far;
										num4 = num2 - 2f;
										break;
									}
									if (current.ValueFormat == FormatType.UpDownSymbol)
									{
										if (this._flForDraw.Text.ToString() == "+")
										{
											g.DrawString("é", new Font("Wingdings", this.Font.Size - 1f, this.Font.Style), Brushes.Lime, dX + num4, num + 2f, this.stringFormat);
										}
										else
										{
											if (this._flForDraw.Text.ToString() == "-")
											{
												g.DrawString("ê", new Font("Wingdings", this.Font.Size - 1f, this.Font.Style), Brushes.Red, dX + num4, num + 2f, this.stringFormat);
											}
										}
									}
									else
									{
										text = this._flForDraw.Text.ToString();
										if (current.ValueFormat == FormatType.RecordNumber)
										{
											text = recordNoParent.ToString();
										}
										Brush brush2 = this._flForDraw.FontBrush;
										if (this.canBlink && this._flForDraw.IsBlink != 0)
										{
											brush2 = Brushes.White;
										}
										g.DrawString(text, new Font(this.Font, this.Font.Style | this._flForDraw.FontStyle), brush2, dX + num4, num + 2f, this.stringFormat);
										if (current.ValueFormat == FormatType.Symbol && !string.IsNullOrEmpty(this._flForDraw.Tag.ToString()))
										{
											num4 = g.MeasureString(text, new Font(this.Font, this.Font.Style | this._flForDraw.FontStyle)).Width;
											using (Font font = new Font(this.Font.Name, this.Font.Size - (float)((double)this.Font.Size * 0.3), FontStyle.Regular))
											{
												g.DrawString(" (" + this._flForDraw.Tag.ToString() + ")", font, Brushes.Orange, dX + num4 - 1f, num + (float)((this.Font.Height - font.Height) / 2) + 2f, this.stringFormat);
											}
										}
									}
								}
								this._flForDraw.Changed = false;
							}
							num3++;
							if (num3 == 2 && this._hScrollVisible)
							{
								g.SetClip(new RectangleF(num2, num + 1f, this._formWidth, this._rowHeight - 1f));
							}
						}
					}
					if (this._hScrollVisible)
					{
						g.ResetClip();
					}
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
			this.columnHeight = (float)Math.Ceiling((double)(this._rowHeight * (this.headerPctHeight / 100f)));
			if (this.isDrawHeader)
			{
				this._beginY = this.columnHeight;
				this.vScrollbar.HeaderHeight = this.columnHeight;
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
		private string VolumeFormat(object volume, bool comma)
		{
			return FormatUtil.VolumeFormat(volume, comma);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string PriceFormat(object price, bool sign, string unit = "")
		{
			return FormatUtil.PriceFormat(price, sign, unit);
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
					num -= this.columnHeight;
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
				if (this._records.Count > 0 && num3 <= this._records[this._records.Count - 1].Index + this._records[this._records.Count - 1].Rows - 1)
				{
					int num4 = base.Width;
					if (this.isScrollable)
					{
						num4 -= this.vScrollbar.Width;
					}
					for (int i = 0; i < this._records.Count; i++)
					{
						if (num3 >= this._records[i].Index && num3 <= this._records[i].Index + this._records[i].Rows - 1)
						{
							num3 = i;
							break;
						}
					}
				}
				ColumnItem column = null;
				foreach (ColumnItem current in this._columns)
				{
					if (current.Visible && ((float)e.X >= current.dX & (float)e.X <= current.dX + current.dW))
					{
						column = current;
						break;
					}
				}
				result = new TableMouseEventArgs(e, num3, column);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Rectangle GetFieldPosition(int rowIndex, string fieldName)
		{
			Rectangle result = default(Rectangle);
			try
			{
				float num = (float)(this._records[rowIndex].Index - this.CurrentScroll - 1) * this._rowHeight;
				if (this.IsDrawHeader)
				{
					num += this.columnHeight;
				}
				if (num < this._beginY)
				{
					num = this._beginY;
				}
				foreach (ColumnItem current in this._columns)
				{
					if (current.Name == fieldName)
					{
						result = new Rectangle((int)current.dX, (int)num, (int)current.dW, (int)this._rowHeight);
						break;
					}
				}
			}
			catch
			{
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
				if (this.isAutoRepaint && this._rowCount > 0)
				{
					using (Graphics graphics = base.CreateGraphics())
					{
						bool flag = false;
						for (int i = this._startIndex; i <= this._endDrawIndex; i++)
						{
							flag = false;
							RecordItem recordItem = this._records[i - 1];
							foreach (ColumnItem current in this._columns)
							{
								FieldItem fieldItem = recordItem.Fields(current.Name);
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
							if (recordItem.Rows > 1)
							{
								int num = 1;
								foreach (SubRecordItem current2 in recordItem.SubRecord)
								{
									flag = false;
									foreach (ColumnItem current3 in this._columns)
									{
										FieldItem fieldItem = current2.Fields(current3.Name);
										if (fieldItem.IsBlink != 0 && fieldItem.LastBlink.AddMilliseconds(1000.0) <= DateTime.Now)
										{
											fieldItem.IsBlink = 0;
											fieldItem.Changed = true;
											flag = true;
										}
									}
									if (flag)
									{
										this.DrawSubRecord(graphics, i, num, false);
									}
									num++;
								}
							}
							recordItem = null;
						}
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void EndUpdate(int recordIndex)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ExpandGrid.EndUpdateCallBack(this.EndUpdate), new object[]
				{
					recordIndex
				});
				return;
			}
			try
			{
				if (recordIndex + 1 >= this._startIndex && recordIndex + 1 <= this._rowCount)
				{
					using (Graphics graphics = base.CreateGraphics())
					{
						RecordItem recordItem = this._records[recordIndex];
						if (this.DrawRecord(graphics, recordIndex + 1, recordItem.Changed) && recordItem.Rows > 1)
						{
							for (int i = 0; i < recordItem.SubRecord.Count; i++)
							{
								this.DrawSubRecord(graphics, recordIndex + 1, i + 1, recordItem.SubRecord[i].Changed);
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
				this.FocusItemIndex = -1;
				if (this._records != null && this._records.Count > 0)
				{
					this.CurrentScroll = 0;
					if (this.vScrollbar.Maximum != this._rowCount)
					{
						this.vScrollbar.Maximum = this._rowCount;
					}
					int num = this._startDrawIndex;
					foreach (RecordItem current in this._records)
					{
						foreach (ColumnItem current2 in this._columns)
						{
							if (current2.ValueFormat != FormatType.Label)
							{
								FieldItem fieldItem = current.Fields(current2.Name);
								fieldItem.Text = string.Empty;
								fieldItem.TextFront = string.Empty;
								if (current2.ValueFormat != FormatType.RecordNumber)
								{
									fieldItem.FontColor = Color.White;
								}
								fieldItem.BackColor = Color.Transparent;
								fieldItem.Tag = string.Empty;
								fieldItem.Changed = true;
								fieldItem.IsBlink = 0;
								fieldItem.FontStyle = FontStyle.Regular;
							}
						}
						if (current.Rows > 1)
						{
							current.Rows = 1;
							current.ClearSubRecord();
						}
						current.Index = num;
						num++;
						current.Changed = true;
					}
				}
				if (this.isScrollable)
				{
					this.vScrollbar.Value = 0;
				}
				this.SetStartDrawIndex();
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
				RecordItem recordItem = this._records[rowIndex];
				foreach (ColumnItem current in this._columns)
				{
					if (current.ValueFormat != FormatType.Label)
					{
						recordItem.Fields(current.Name).Text = string.Empty;
						recordItem.Fields(current.Name).IsBlink = 0;
						recordItem.Fields(current.Name).Tag = string.Empty;
						recordItem.Fields(current.Name).FontStyle = FontStyle.Regular;
						recordItem.Fields(current.Name).BackColor = Color.Transparent;
						recordItem.Fields(current.Name).TextFront = string.Empty;
						if (current.ValueFormat != FormatType.RecordNumber)
						{
							recordItem.Fields(current.Name).FontColor = Color.White;
						}
					}
				}
				if (recordItem.Rows > 1)
				{
					recordItem.Rows = 1;
					recordItem.ClearSubRecord();
					int num = recordItem.Index + recordItem.Rows;
					for (int i = rowIndex + 1; i < this._rowCount; i++)
					{
						this._records[i].Index = num;
						num += this._records[i].Rows;
					}
					this.SetStartDrawIndex();
					this.vScrollbar.Maximum = this._rowCount;
				}
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int GetHeightByRows()
		{
			return this.GetHeightByRows(this._rowCount);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int GetHeightByRows(int recordPerPage)
		{
			if (this.isDrawHeader)
			{
				return (int)(this._rowHeight * (float)recordPerPage + this.columnHeight + 2f);
			}
			return (int)(this._rowHeight * (float)recordPerPage + 2f);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int GetRowCountByHeight()
		{
			int result;
			try
			{
				if (this._rowHeight > 0f)
				{
					result = this.GetRowCountPerPage();
				}
				else
				{
					result = 0;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetRowByHeight()
		{
			try
			{
				this.Rows = this.GetRowCountByHeight();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RecordItem Records(int index)
		{
			return this._records[index];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RecordItem AddRecord(int position, bool isFixSize)
		{
			RecordItem result;
			try
			{
				RecordItem recordItem;
				if (position == -1)
				{
					position = this._records.Count + 1;
					recordItem = new RecordItem(ref this._columns, position - 1, this.BackColor);
					this._records.Add(recordItem);
				}
				else
				{
					if (position > this._records.Count + 1)
					{
						result = null;
						return result;
					}
					recordItem = new RecordItem(ref this._columns, position - 1, this.BackColor);
					this._records.Insert(position - 1, recordItem);
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
				}
				int num = position;
				for (int i = position - 1; i < this._rowCount; i++)
				{
					this._records[i].Changed = true;
					this._records[i].Index = num;
					num += this._records[i].Rows;
				}
				if (this.isScrollable)
				{
					if (!isFixSize)
					{
						this.vScrollbar.Maximum++;
					}
					if (!isFixSize && this.FocusItemIndex > -1)
					{
						this.SetFocusItem(this.FocusItemIndex + 1);
					}
					this.SetLargeChangeAndEnable();
				}
				this.SetStartDrawIndex();
				result = recordItem;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
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
				if (colName != string.Empty && this.ColumnExist(colName))
				{
					this.sortColumeName = colName;
					this.sortType = sortBy;
					this.Sort();
					int num = this._startIndex;
					foreach (RecordItem current in this._records)
					{
						current.Index = num;
						num += current.Rows;
					}
					this.SetStartDrawIndex();
				}
				else
				{
					this.sortColumeName = string.Empty;
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
					if (this._rowCount > 0)
					{
						for (int i = this._startDrawIndex; i <= this._endDrawIndex; i++)
						{
							if (this.DrawRecord(e.Graphics, i, true) && this._records[i - 1].Rows > 1)
							{
								int num = 0;
								foreach (SubRecordItem arg_B3_0 in this._records[i - 1].SubRecord)
								{
									this.DrawSubRecord(e.Graphics, i, num + 1, true);
									num++;
								}
							}
						}
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
				if (e.Clicks == 2)
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
					if (e.Clicks == 1)
					{
						TableMouseEventArgs tableMouseArgument = this.GetTableMouseArgument(e);
						if (tableMouseArgument != null)
						{
							if (e.Button == MouseButtons.Left && tableMouseArgument.Column.IsExpand && (float)e.X <= tableMouseArgument.Column.dX + 10f && !string.IsNullOrEmpty(this._records[tableMouseArgument.RowIndex].Fields(tableMouseArgument.Column.Name).Text.ToString()))
							{
								if (this._TableClickExpand != null)
								{
									this._TableClickExpand(this, tableMouseArgument);
								}
							}
							else
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
								if (tableMouseArgument.RowIndex > -1 && this.rowSelectType > 1)
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
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this._isMouseDown = true;
			this._isFirstMouseMove = true;
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
				if (this._isMouseDown)
				{
					if (this.CanDrag && this._isFirstMouseMove && (this.mousePoint.X != e.X || this.mousePoint.Y != e.Y))
					{
						this._isFirstMouseMove = false;
						if (this.OnFieldItemDrag(e))
						{
							base.DoDragDrop(this.GetDataForDragDrop(), DragDropEffects.Move);
						}
					}
				}
				else
				{
					if (this.canGetMouseMove)
					{
						if ((float)e.Y < this._rowHeight)
						{
							TableMouseEventArgs tableMouseArgument = this.GetTableMouseArgument(e);
							if (tableMouseArgument != null && this._TableHeaderMouseMove != null)
							{
								this._TableHeaderMouseMove(tableMouseArgument);
							}
						}
						else
						{
							if (this._TableHeaderMouseMove != null)
							{
								this._TableHeaderMouseMove(null);
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
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			try
			{
				this._isMouseDown = false;
				this._isFirstMouseMove = false;
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual bool OnFieldItemDrag(MouseEventArgs e)
		{
			bool result;
			try
			{
				FieldItemDragEventArgs fieldItemDragEventArgs = this.CreateFieldItemDragEventArgs(e);
				if (fieldItemDragEventArgs == null || fieldItemDragEventArgs.Item.Text == null || fieldItemDragEventArgs.Item.Text.ToString() == string.Empty)
				{
					result = false;
				}
				else
				{
					this.dragItem = fieldItemDragEventArgs.Item;
					this.isDraging = true;
					this.isDroped = false;
					this.dragFlake = new FlakeDlg(this.dragItem.Text.ToString(), this.dragItem.FontColor);
					this.timerDragdrop.Start();
					result = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
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
				if (!this.isPainting && this._ItemDragDrop != null)
				{
					DragItemData dragItemData = (DragItemData)e.Data.GetData(typeof(DragItemData).ToString());
					if (dragItemData.DragText != null)
					{
						Point point = base.PointToClient(new Point(e.X, e.Y));
						string dragValue = dragItemData.DragText.ToString();
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
			if (this.dragItem.Text != null)
			{
				dragItemData.DragText = this.dragItem.Text.ToString();
			}
			else
			{
				dragItemData.DragText = string.Empty;
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
				if (num > -1 & num <= this._rowCount)
				{
					Rectangle fieldPosition = default(Rectangle);
					int num2 = 0;
					foreach (ColumnItem current in this._columns)
					{
						if (current.Visible)
						{
							if ((float)e.X >= current.dX & (float)e.X <= current.dX + current.dW)
							{
								int y = Convert.ToInt32((float)(num + 1) * this._rowHeight);
								int width = Convert.ToInt32((double)(current.Width * base.Width) / 100.0);
								int height = Convert.ToInt32(this._rowHeight);
								fieldPosition = new Rectangle(num2, y, width, height);
								text = current.Name;
								FormatType arg_12B_0 = current.ValueFormat;
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
				if (num > -1 & num <= this._rowCount)
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
		private int GetRowCountPerPage()
		{
			int result = 0;
			try
			{
				if (this._rowHeight > 0f)
				{
					float num = (float)base.Height;
					if (this.isDrawHeader)
					{
						num -= this.columnHeight;
					}
					if (this._hScrollVisible)
					{
						num -= (float)this.hScrollbar.Height;
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
				{
					int num = -1;
					int num2 = 0;
					int rowCountPerPage = this.GetRowCountPerPage();
					for (int i = this._startDrawIndex - 1; i > 0; i--)
					{
						num2 += this._records[i - 1].Rows;
						if (num2 >= rowCountPerPage)
						{
							num = i;
							break;
						}
					}
					if (num == -1)
					{
						num = 1;
					}
					this.CurrentScroll = this._records[num - 1].Index - 1;
					e.SuppressKeyPress = true;
					return;
				}
				case Keys.Next:
					if (this._endDrawIndex <= this._rowCount)
					{
						int num3 = this.vScrollbar.Maximum - this._records[this._endDrawIndex - 1].Index;
						int rowCountPerPage = this.GetRowCountPerPage();
						int num2 = this._records[this._startDrawIndex - 1].Index + this._records[this._startDrawIndex - 1].Rows - 1 - this.CurrentScroll;
						int num4 = this._startDrawIndex;
						while (num4 <= this._endDrawIndex && num4 < this._rowCount)
						{
							num2 += this._records[num4].Rows;
							num4++;
						}
						num3 += num2 - rowCountPerPage;
						int num5;
						if (num3 > rowCountPerPage)
						{
							num5 = rowCountPerPage;
						}
						else
						{
							num5 = num3;
						}
						this.CurrentScroll += num5;
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
				if (newIndex > -1 && newIndex < this._rowCount)
				{
					if (newIndex != this.FocusItemIndex)
					{
						int num = this.FocusItemIndex;
						this.FocusItemIndex = newIndex;
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
						if (this.isScrollable)
						{
							int rowCountPerPage = this.GetRowCountPerPage();
							if (this._records[this.focusItemIndex].Index + this._records[this.focusItemIndex].Rows - 1 > this.CurrentScroll + rowCountPerPage)
							{
								this.CurrentScroll += this._records[this.focusItemIndex].Index + this._records[this.focusItemIndex].Rows - 1 - (this.CurrentScroll + rowCountPerPage);
							}
							else
							{
								if (this._records[this.focusItemIndex].Index <= this.CurrentScroll)
								{
									if (this._records[this.focusItemIndex].Index - 1 < this.CurrentScroll)
									{
										this.CurrentScroll = this._records[this.focusItemIndex].Index - 1;
									}
									else
									{
										this.CurrentScroll = 0;
									}
								}
							}
						}
						if (this.rowSelectType > 1 && this._TableFocusIndexChanged != null)
						{
							this._TableFocusIndexChanged(this, this.focusItemIndex);
						}
					}
					else
					{
						if (this.isAutoRepaint)
						{
							this.EndUpdate(newIndex);
						}
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ClearFocusItem()
		{
			if (this.FocusItemIndex > -1 && this.FocusItemIndex < this._rowCount)
			{
				int num = this.FocusItemIndex;
				this.FocusItemIndex = -1;
				this._records[num].Changed = true;
				this.EndUpdate(num);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Rectangle GetPosition(int rowIndex, string columnName)
		{
			Rectangle result = default(Rectangle);
			try
			{
				if (rowIndex > -1 & rowIndex <= this._rowCount)
				{
					int num = 0;
					int num2 = base.Width;
					if (this.isScrollable)
					{
						num2 -= this.vScrollbar.Width;
					}
					foreach (ColumnItem current in this._columns)
					{
						if (current.Visible)
						{
							if (current.Name == columnName)
							{
								result = new Rectangle(num, Convert.ToInt32((float)rowIndex * this._rowHeight), Convert.ToInt32((double)(current.Width * base.Width) / 100.0), Convert.ToInt32(this._rowHeight));
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
		public SubRecordItem AddSubRecord(int recordNo)
		{
			SubRecordItem result;
			try
			{
				result = this._records[recordNo].AddSubRecord(ref this._columns);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void hScrollbar_ValueChanged(object sender, EventArgs e)
		{
			base.Invalidate(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void hScrollbar_VisibleChanged(object sender, EventArgs e)
		{
			this._hScrollVisible = this.hScrollbar.Visible;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			try
			{
				this.isAutoRepaint = false;
				this._flForDraw = null;
				this.Columns.Clear();
				this.Columns = null;
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
				if (this.hScrollbar != null)
				{
					this.hScrollbar.Dispose();
					this.hScrollbar = null;
				}
			}
			catch
			{
			}
		}
	}
}
