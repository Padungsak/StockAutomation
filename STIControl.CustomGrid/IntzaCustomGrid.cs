using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace STIControl.CustomGrid
{
	[Designer(typeof(IntzaCustomGridDesigner))]
	public class IntzaCustomGrid : UserControl, IItemContainer
	{
		public delegate void ItemDragEventHandler(object sender, ItemGridMouseEventArgs e);
		public delegate void ItemDragDropEventHandler(object sender, ItemGridMouseEventArgs e, string dragValue);
		public delegate void GridMouseClickEventHandler(object sender, ItemGridMouseEventArgs e);
		public delegate void GridMouseDoubleClickEventHandler(object sender, ItemGridMouseEventArgs e);
		private bool autoRepaint = true;
		private StringFormat stringFormat = new StringFormat();
		private float textHeigth;
		private System.Windows.Forms.Timer tmBlink;
		private bool isMouseDown;
		private bool isFirstMouseMove;
		private Point mousePoint = default(Point);
		private System.Windows.Forms.Timer timerDrag;
		private bool isDraging;
		private FlakeDlg dragFlake;
		private ItemGrid dragItem;
		private List<ItemGrid> items;
		private bool canDrag;
		private Color lineColor = Color.Red;
		private bool isDroped;
		private object _objLock = new object();
        public IntzaCustomGrid.ItemDragDropEventHandler _ItemDragDrop;
		public event IntzaCustomGrid.ItemDragDropEventHandler ItemDragDrop
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
        public IntzaCustomGrid.GridMouseClickEventHandler _GridMouseClick;
		public event IntzaCustomGrid.GridMouseClickEventHandler GridMouseClick
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GridMouseClick += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GridMouseClick -= value;
			}
		}
        public IntzaCustomGrid.GridMouseDoubleClickEventHandler _GridMouseDoubleClick;
		public event IntzaCustomGrid.GridMouseDoubleClickEventHandler GridMouseDoubleClick
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GridMouseDoubleClick += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GridMouseDoubleClick -= value;
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<ItemGrid> Items
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.items;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.items = value;
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
				base.Font = value;
				this.SetTextH();
			}
		}
		public bool IsAutoRepaint
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.autoRepaint;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.autoRepaint = value;
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
		public Color LineColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lineColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.lineColor = value;
			}
		}
		public bool IsDroped
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isDroped;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isDroped = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IntzaCustomGrid()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			this.SetTextH();
			base.UpdateStyles();
			this.items = new List<ItemGrid>();
			if (!base.DesignMode)
			{
				this.timerDrag = new System.Windows.Forms.Timer();
				this.timerDrag.Interval = 15;
				this.timerDrag.Tick += new EventHandler(this.timer_Tick);
				this.tmBlink = new System.Windows.Forms.Timer();
				this.tmBlink.Interval = 400;
				this.tmBlink.Tick -= new EventHandler(this.tmBlink_Tick);
				this.tmBlink.Tick += new EventHandler(this.tmBlink_Tick);
				this.tmBlink.Start();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			base.SuspendLayout();
			this.AllowDrop = true;
			base.Name = "IntzaCustomGrid";
			base.ResumeLayout(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void Dispose(bool disposing)
		{
			this.autoRepaint = false;
			if (this.tmBlink != null)
			{
				this.tmBlink.Enabled = false;
				this.tmBlink.Dispose();
				this.tmBlink = null;
			}
			if (this.timerDrag != null)
			{
				this.timerDrag.Stop();
				this.timerDrag.Dispose();
				this.timerDrag = null;
			}
			this.dragItem = null;
			if (this.dragFlake != null)
			{
				this.dragFlake.Dispose();
				this.dragFlake = null;
			}
			if (this.items != null)
			{
				this.items.Clear();
				this.items = null;
			}
			this.stringFormat.Dispose();
			base.Dispose(disposing);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmBlink_Tick(object sender, EventArgs e)
		{
			try
			{
				using (Graphics graphics = base.CreateGraphics())
				{
					foreach (ItemGrid current in this.items)
					{
						if (current.IsBlink != 0 && current.LastBlink.AddMilliseconds(1000.0) <= DateTime.Now)
						{
							current.IsBlink = 0;
							if (this.autoRepaint)
							{
								this.DrawItem(current, graphics);
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
		private void DrawItem(ItemGrid item, Graphics g)
		{
			try
			{
				if (item.Visible)
				{
					float num = (float)((double)(item.Width * base.ClientSize.Width) / 100.0);
					float num2 = (float)((double)(item.X * base.ClientSize.Width) / 100.0);
					float y = (float)item.Y * this.textHeigth;
					RectangleF rectangleF = new RectangleF(num2, y, num, this.textHeigth * (float)item.Height);
					rectangleF.Width -= 1f;
					g.SetClip(rectangleF);
					if (item.ValueFormat == FormatType.ScaleBar)
					{
						g.FillRectangle(item.BackBrush, rectangleF);
						if (!string.IsNullOrEmpty(item.Text))
						{
							num -= 2f;
							this.stringFormat.Alignment = StringAlignment.Far;
							float width = num * Convert.ToSingle(item.Text) / 100f;
							g.FillRectangle(Brushes.Silver, num2, rectangleF.Y + 1f, num, rectangleF.Height - 2f);
							g.FillRectangle(item.FontBrush, num2, rectangleF.Y + 1f, width, rectangleF.Height - 2f);
							rectangleF.Y += 2f;
							g.DrawString(item.Text + "%", new Font(this.Font, this.Font.Style | item.FontStyle), (item.IsBlink == 1) ? Brushes.White : Brushes.Black, rectangleF, this.stringFormat);
						}
					}
					else
					{
						if (item.ValueFormat == FormatType.BidOfferPct)
						{
							g.FillRectangle(item.BackBrush, rectangleF);
							if (!string.IsNullOrEmpty(item.Text))
							{
								num -= 6f;
								float num3 = num * Convert.ToSingle(item.Text) / 100f;
								if (num3 > 0f)
								{
									using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new RectangleF(num2 + 3f, rectangleF.Y + 1f, num3, rectangleF.Height - 2f), Color.Cyan, Color.FromArgb(70, Color.Cyan), LinearGradientMode.Vertical))
									{
										g.FillRectangle(linearGradientBrush, new RectangleF(num2 + 3f, rectangleF.Y + 1f, num3, rectangleF.Height - 2f));
									}
								}
								if (num - num3 > 0f)
								{
									using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new RectangleF(num2 + 3f + num3, rectangleF.Y + 1f, num - num3, rectangleF.Height - 2f), Color.Magenta, Color.FromArgb(70, Color.Magenta), LinearGradientMode.Vertical))
									{
										g.FillRectangle(linearGradientBrush2, new RectangleF(num2 + 3f + num3, rectangleF.Y + 1f, num - num3, rectangleF.Height - 2f));
									}
								}
								rectangleF.Y += rectangleF.Height * 0.1f;
								rectangleF.X += 3f;
								rectangleF.Width -= 6f;
								this.stringFormat.Alignment = StringAlignment.Near;
								g.DrawString(item.Text + "%", new Font(this.Font.Name, this.Font.Size - 1f, this.Font.Style), (item.IsBlink == 1) ? Brushes.LightGray : Brushes.White, rectangleF, this.stringFormat);
								this.stringFormat.Alignment = StringAlignment.Far;
								decimal d;
								decimal.TryParse(item.Text.ToString(), out d);
								g.DrawString(100m - d + "%", new Font(this.Font.Name, this.Font.Size - 1f, this.Font.Style), (item.IsBlink == 1) ? Brushes.LightGray : Brushes.White, rectangleF, this.stringFormat);
							}
						}
						else
						{
							if (item.ValueFormat == FormatType.PieChart)
							{
								g.SmoothingMode = SmoothingMode.HighQuality;
								g.FillRectangle(item.BackBrush, rectangleF);
								if (!string.IsNullOrEmpty(item.Text.ToString()))
								{
									string[] array = item.Text.ToString().Split(new char[]
									{
										';'
									});
									Rectangle rect = new Rectangle((int)rectangleF.Left + 1, (int)rectangleF.Top + 1, (int)rectangleF.Height - 2, (int)rectangleF.Height - 2);
									float num4 = (float)Math.Round((double)(Convert.ToSingle(array[0]) * 3.6f), 1);
									if (num4 > 0f)
									{
										g.FillPie(Brushes.Yellow, rect, 180f, num4);
									}
									float num5 = (float)Math.Round((double)(Convert.ToSingle(array[1]) * 3.6f), 1);
									if (num5 > 0f)
									{
										g.FillPie(Brushes.Lime, rect, 180f + num4, num5);
									}
									float num6 = Convert.ToSingle(array[2]);
									if (num6 > 0f)
									{
										num6 = 360f - (num4 + num5) + 1f;
										g.FillPie(Brushes.Red, rect, 180f, -num6);
									}
								}
							}
							else
							{
								Brush brush = item.FontBrush;
								if (item.FieldType == ItemType.TextGradient)
								{
									g.FillRectangle(Brushes.Black, rectangleF);
									using (LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(new RectangleF(num2, y, rectangleF.Width, rectangleF.Height), item.BackColor, Color.FromArgb(80, item.BackColor), LinearGradientMode.Vertical))
									{
										g.FillRectangle(linearGradientBrush3, num2, y, rectangleF.Width, rectangleF.Height);
										goto IL_6AA;
									}
								}
								if (item.IsBlink == 0)
								{
									g.FillRectangle(item.BackBrush, rectangleF);
								}
								else
								{
									if (item.IsBlink == 1)
									{
										brush = Brushes.White;
										g.FillRectangle(item.BackBrush, rectangleF);
									}
									else
									{
										if (item.IsBlink == 2)
										{
											brush = Brushes.White;
											g.FillRectangle(Brushes.Green, rectangleF);
										}
										else
										{
											if (item.IsBlink == 3)
											{
												brush = Brushes.White;
												g.FillRectangle(Brushes.Red, rectangleF);
											}
										}
									}
								}
								IL_6AA:
								if (item.Text != string.Empty)
								{
									if (item.Alignment == StringAlignment.Near)
									{
										this.stringFormat.Alignment = StringAlignment.Near;
										rectangleF.X = rectangleF.Left + 1f;
									}
									else
									{
										if (item.Alignment == StringAlignment.Far)
										{
											this.stringFormat.Alignment = StringAlignment.Far;
											rectangleF.Width -= 3f;
										}
										else
										{
											if (item.Alignment == StringAlignment.Center)
											{
												this.stringFormat.Alignment = StringAlignment.Center;
											}
										}
									}
									rectangleF.Y += 2f;
									g.DrawString(item.Text, new Font(this.Font.Name, this.Font.Size + item.AdjustFontSize, this.Font.Style | item.FontStyle), brush, rectangleF, this.stringFormat);
									if (item.FieldType == ItemType.Label2)
									{
										this.stringFormat.Alignment = StringAlignment.Far;
										g.DrawString(":", new Font(this.Font, this.Font.Style | item.FontStyle), brush, rectangleF, this.stringFormat);
									}
								}
								brush = null;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				item.Changed = false;
				g.ResetClip();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetTextH()
		{
			this.textHeigth = (float)((int)Math.Ceiling((double)base.CreateGraphics().MeasureString("X", this.Font).Height)) + 2f;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BeginUpdate()
		{
			this.autoRepaint = false;
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
				using (Graphics graphics = base.CreateGraphics())
				{
					this.autoRepaint = true;
					foreach (ItemGrid current in this.items)
					{
						if (current.Changed)
						{
							this.DrawItem(current, graphics);
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
		public void Redraw()
		{
			this.autoRepaint = true;
			base.Invalidate(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ClearAllText()
		{
			try
			{
				foreach (ItemGrid current in this.items)
				{
					if (current.FieldType == ItemType.Text)
					{
						current.Text = string.Empty;
						current.Changed = true;
					}
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
			try
			{
				int num = 0;
				foreach (ItemGrid current in this.items)
				{
					if (current.Y > num)
					{
						num = current.Y;
					}
				}
				return (int)Math.Round((double)(this.textHeigth * (float)(num + 1) + 1f));
			}
			catch
			{
			}
			return 0;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ItemGrid Item(string keyItemName)
		{
			ItemGrid result;
			try
			{
				object objLock;
				Monitor.Enter(objLock = this._objLock);
				try
				{
					result = this.items.Find((ItemGrid item) => item.Name == keyItemName);
				}
				finally
				{
					Monitor.Exit(objLock);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			try
			{
				if (this.autoRepaint)
				{
					foreach (ItemGrid current in this.items)
					{
						this.DrawItem(current, e.Graphics);
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			ItemGridMouseEventArgs itemGridMouseEventArgs = this.CreateFieldItemDragEventArgs(e);
			if (itemGridMouseEventArgs != null && this._GridMouseDoubleClick != null)
			{
				this._GridMouseDoubleClick(this, itemGridMouseEventArgs);
			}
			base.OnMouseDoubleClick(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.isMouseDown = true;
			this.isFirstMouseMove = true;
			this.mousePoint.X = e.X;
			this.mousePoint.Y = e.Y;
			base.OnMouseDown(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.CanDrag && this.isMouseDown && this.isFirstMouseMove && (this.mousePoint.X != e.X || this.mousePoint.Y != e.Y))
			{
				this.isFirstMouseMove = false;
				if (this.OnFieldItemDrag(e))
				{
					base.DoDragDrop(this.GetDataForDragDrop(), DragDropEffects.Move);
				}
			}
			base.OnMouseMove(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnMouseUp(MouseEventArgs e)
		{
			try
			{
				if (this.isFirstMouseMove && this._GridMouseClick != null)
				{
					ItemGridMouseEventArgs itemGridMouseEventArgs = this.CreateFieldItemDragEventArgs(e);
					if (itemGridMouseEventArgs != null)
					{
						this._GridMouseClick(this, itemGridMouseEventArgs);
					}
				}
				this.isMouseDown = false;
				this.isFirstMouseMove = false;
			}
			catch
			{
			}
			finally
			{
				base.OnMouseUp(e);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual bool OnFieldItemDrag(MouseEventArgs e)
		{
			try
			{
				ItemGridMouseEventArgs itemGridMouseEventArgs = this.CreateFieldItemDragEventArgs(e);
				if (itemGridMouseEventArgs == null)
				{
					bool result = false;
					return result;
				}
				this.dragItem = itemGridMouseEventArgs.Item;
				if (this.dragItem.FieldType == ItemType.Label)
				{
					bool result = false;
					return result;
				}
				int width = (int)((double)(itemGridMouseEventArgs.Item.Width * base.ClientSize.Width) / 100.0);
				int height = (int)(this.textHeigth * (float)itemGridMouseEventArgs.Item.Height);
				Bitmap bitmap = new Bitmap(width, height);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.DrawString(this.dragItem.Text, new Font(this.Font.Name, 10f, FontStyle.Bold), new SolidBrush(this.dragItem.FontColor), 1f, 1f);
				this.isDraging = true;
				this.IsDroped = false;
				this.dragFlake = new FlakeDlg(bitmap);
				this.timerDrag.Start();
			}
			catch
			{
			}
			return true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
		{
			if (this.CanDrag && qcdevent.Action == DragAction.Drop && qcdevent.KeyState == 0)
			{
				this.Cursor = Cursors.Default;
				this.IsDroped = true;
			}
			base.OnQueryContinueDrag(qcdevent);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnDragEnter(DragEventArgs e)
		{
			try
			{
				if (this.CanDrag)
				{
					if (!e.Data.GetDataPresent(typeof(DragItemData).ToString()))
					{
						e.Effect = DragDropEffects.None;
					}
					else
					{
						if (this.dragItem != null && this.dragItem.FieldType == ItemType.Label)
						{
							e.Effect = DragDropEffects.None;
							this.Cursor = Cursors.No;
						}
						else
						{
							this.Cursor = Cursors.Default;
							e.Effect = DragDropEffects.Move;
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				base.OnDragEnter(e);
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
			try
			{
				base.Invalidate();
			}
			catch
			{
			}
			finally
			{
				base.OnDragLeave(e);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnDragDrop(DragEventArgs e)
		{
			try
			{
				this.isDraging = false;
				if (this._ItemDragDrop != null)
				{
					DragItemData dragItemData = (DragItemData)e.Data.GetData(typeof(DragItemData).ToString());
					Point point = base.PointToClient(new Point(e.X, e.Y));
					string dragValue = string.Empty;
					if (dragItemData != null && dragItemData.DragText != null)
					{
						dragValue = dragItemData.DragText;
					}
					this._ItemDragDrop(this, this.CreateFieldItemDragEventArgs(new MouseEventArgs(MouseButtons.Left, 0, point.X, point.Y, 0)), dragValue);
				}
			}
			catch
			{
			}
			finally
			{
				base.OnDragDrop(e);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnLostFocus(EventArgs e)
		{
			base.Invalidate();
			base.OnLostFocus(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnFontChanged(EventArgs e)
		{
			this.SetTextH();
			base.OnFontChanged(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private DragItemData GetDataForDragDrop()
		{
			DragItemData dragItemData = new DragItemData(this);
			if (this.dragItem != null && this.dragItem.Text != null)
			{
				dragItemData.DragText = this.dragItem.Text;
			}
			else
			{
				dragItemData.DragText = string.Empty;
			}
			return dragItemData;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private ItemGridMouseEventArgs CreateFieldItemDragEventArgs(MouseEventArgs e)
		{
			ItemGridMouseEventArgs result = null;
			try
			{
				ItemGrid itemAt = this.GetItemAt(new Point(e.X, e.Y));
				if (itemAt != null && itemAt.Text != null)
				{
					result = new ItemGridMouseEventArgs(e, itemAt);
				}
			}
			catch
			{
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private ItemGrid GetItemAt(Point point)
		{
			return this.GetItemAt(point.X, point.Y);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private ItemGrid GetItemAt(int positionX, int positionY)
		{
			ItemGrid result = null;
			try
			{
				Point p = new Point(positionX, positionY);
				foreach (ItemGrid current in this.items)
				{
					float width = (float)((double)(current.Width * base.ClientSize.Width) / 100.0);
					float height = this.textHeigth * (float)current.Height;
					float x = (float)((double)(current.X * base.ClientSize.Width) / 100.0);
					float y = (float)current.Y * this.textHeigth;
					if (new RectangleF(x, y, width, height).Contains(p))
					{
						result = current;
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
		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.dragFlake != null)
			{
				if (this.IsDroped)
				{
					this.dragFlake.Close();
					this.dragFlake = null;
					base.Invalidate(true);
					this.timerDrag.Stop();
					this.isDraging = false;
					return;
				}
				if (this.isDraging)
				{
					this.dragFlake.Show(Control.MousePosition.X - 1, Control.MousePosition.Y + 1);
					return;
				}
				this.dragFlake.Close();
				this.dragFlake = null;
				base.Invalidate(true);
				this.timerDrag.Stop();
				this.isDraging = false;
			}
		}
	}
}
