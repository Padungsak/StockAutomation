using ITSNet.Common.BIZ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
public class ucVolumeAtPrice : UserControl
{
	public delegate void ActivatedEventHandler();
	public delegate void DragEventHandler(string Symbol, Color Color);
	public delegate void ItemChangeEventHandler(string strSymbol, string strTypeMode, string strInterval, string strStartDate, string strEndDate);
	public delegate void StartGetWsEventHandler(string Symbol, string TypeSymbol);
	public delegate void LoadCompleteEventHandler(string Symbol, string TypeSymbol);
	public delegate void SendErrorMsgEventHandler(string SubName, string ErrorMsg);
	public delegate void OpenWithEventHandler(string Symbol);
	public delegate void EnableLoadingEventHandler();
	public delegate void DisableloadingEventHandler();
	private PictureBox PictureBox1;
	private bool bLoaded;
	private clsGraphPanel oGraphPanel = new clsGraphPanel();
	public List<string> SymbolListTfex;
	public List<string> SymbolListSet;
	private string strEndDate = string.Empty;
	private enumMode eTypeMode = enumMode.Previous;
	private enumType eOldSymbolType;
	private string strCurDate;
	private Color m_TextBoxBgColor;
	private Color m_TextBoxForeColor;
	private int _mode;
	public ucVolumeAtPrice.ActivatedEventHandler _Activated;
	public event ucVolumeAtPrice.ActivatedEventHandler Activated
	{
		[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
		add
		{
            this._Activated += value;
        }
		[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
		remove
		{
            this._Activated -= value;
        }
	}
	public ucVolumeAtPrice.DragEventHandler _Drag;
	public event ucVolumeAtPrice.DragEventHandler Drag
	{
		[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
		add
		{
            this._Drag += value;
        }
		[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
		remove
		{
            this._Drag -= value;
        }
	}
	public ucVolumeAtPrice.SendErrorMsgEventHandler _SendErrorMsg;
	public event ucVolumeAtPrice.SendErrorMsgEventHandler SendErrorMsg
	{
		[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
		add
		{
            this._SendErrorMsg += value;
        }
		[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
		remove
		{
            this._SendErrorMsg -= value;
        }
	}
    public ucVolumeAtPrice.OpenWithEventHandler _OpenWith;
	public event ucVolumeAtPrice.OpenWithEventHandler OpenWith
	{
		[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
		add
		{
            this._OpenWith += value;
		}
		[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
		remove
		{
            this._OpenWith -= value;
        }
	}
	public int Mode
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this._mode;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this._mode = value;
		}
	}
	public Color TextBoxBgColor
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.m_TextBoxBgColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.m_TextBoxBgColor != value)
			{
				this.m_TextBoxBgColor = value;
			}
		}
	}
	public Color TextBoxForeColor
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.m_TextBoxForeColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.m_TextBoxForeColor != value)
			{
				this.m_TextBoxForeColor = value;
			}
		}
	}
	public clsPermission ActiveTFEX
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.oActiveTFEX;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.oGraphPanel.oActiveTFEX = value;
		}
	}
	public clsPermission ActiveSET
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.oActiveSET;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.oGraphPanel.oActiveSET = value;
		}
	}
	public Dictionary<string, float> dictIPO
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.dictIPO;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.oGraphPanel.dictIPO = value;
		}
	}
	public string CurDate
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.strCurDate;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.strCurDate != value)
			{
				this.strCurDate = value;
			}
		}
	}
	public enumMode TypeMode
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.eTypeMode;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.eTypeMode = value;
		}
	}
	public enumType SymbolType
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.oSymbolType;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.oGraphPanel.oSymbolType = value;
		}
	}
	public Color ColorCeiling
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_CeilingColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.oGraphPanel.m_CeilingColor != value)
			{
				this.oGraphPanel.m_CeilingColor = value;
				if (this.bLoaded)
				{
					this.PictureBox1.Invalidate();
				}
			}
		}
	}
	public Color ColorFloor
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_FloorColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.oGraphPanel.m_FloorColor != value)
			{
				this.oGraphPanel.m_FloorColor = value;
				if (this.bLoaded)
				{
					this.PictureBox1.Invalidate();
				}
			}
		}
	}
	public string FontName
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_FontName;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (value != this.oGraphPanel.m_FontName)
			{
				this.oGraphPanel.m_FontName = value;
				if (this.bLoaded)
				{
					this.PictureBox1.Invalidate();
				}
			}
		}
	}
	public float FontSize
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_FontSize;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (value != this.oGraphPanel.m_FontSize)
			{
				this.oGraphPanel.m_FontSize = value;
				if (this.bLoaded)
				{
					this.PictureBox1.Invalidate();
				}
			}
		}
	}
	public Color ColorGrid
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_GridColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.oGraphPanel.m_GridColor != value)
			{
				this.oGraphPanel.m_GridColor = value;
				if (this.bLoaded)
				{
					this.PictureBox1.Invalidate();
				}
			}
		}
	}
	public Color ColorBg
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_BgColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.oGraphPanel.m_BgColor != value)
			{
				this.oGraphPanel.m_BgColor = value;
				this.SetBgColor();
				this.PictureBox1.Invalidate();
			}
		}
	}
	public Color ColorVolume
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_VolumeColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (value != this.oGraphPanel.m_VolumeColor)
			{
				this.oGraphPanel.m_VolumeColor = value;
				this.PictureBox1.Invalidate();
			}
		}
	}
	public Color ColorUp
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_UpColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (value != this.oGraphPanel.m_UpColor)
			{
				this.oGraphPanel.m_UpColor = value;
				this.PictureBox1.Invalidate();
			}
		}
	}
	public Color ColorDown
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_DownColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (value != this.oGraphPanel.m_DownColor)
			{
				this.oGraphPanel.m_DownColor = value;
				this.PictureBox1.Invalidate();
			}
		}
	}
	public Color ColorNoChg
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_NoChgColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (value != this.oGraphPanel.m_NoChgColor)
			{
				this.oGraphPanel.m_NoChgColor = value;
				this.PictureBox1.Invalidate();
			}
		}
	}
	public Color ColorBuy
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.BuyColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (value != this.oGraphPanel.BuyColor)
			{
				this.oGraphPanel.BuyColor = value;
				this.PictureBox1.Invalidate();
			}
		}
	}
	public Color ColorSell
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.SellColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (value != this.oGraphPanel.SellColor)
			{
				this.oGraphPanel.SellColor = value;
				this.PictureBox1.Invalidate();
			}
		}
	}
	public Color ColorValue
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.m_ValueColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (value != this.oGraphPanel.m_ValueColor)
			{
				this.oGraphPanel.m_ValueColor = value;
				this.PictureBox1.Invalidate();
			}
		}
	}
	public string SymbolList
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.oGraphPanel.strSymbolList;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.oGraphPanel.strSymbolList != value)
			{
				if (!this.bLoaded)
				{
					this.oGraphPanel.SymbolList = value;
					this.SetSymbolType();
				}
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void SetSymbolTypeSpecial(enumType enumSymbolType)
	{
		try
		{
			this.SymbolType = enumSymbolType;
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub SetSymbolTypeSpecial ", ex.Message);
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void SetSymbolType()
	{
		try
		{
			if (!string.IsNullOrEmpty(this.oGraphPanel.strSymbolList) && this.SymbolListSet.Contains(this.oGraphPanel.strSymbolList))
			{
				this.SymbolType = enumType.eSet;
			}
			else
			{
				if (!string.IsNullOrEmpty(this.oGraphPanel.strSymbolList) && this.SymbolListTfex.Contains(this.oGraphPanel.strSymbolList))
				{
					this.SymbolType = enumType.eTfex;
				}
			}
			if (this.eOldSymbolType != this.SymbolType)
			{
				this.eOldSymbolType = this.SymbolType;
			}
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub SetSymbolType ", ex.Message);
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void SetGraphHeightWidth()
	{
		try
		{
			this.oGraphPanel.Width = this.PictureBox1.Width;
			this.oGraphPanel.Height = this.PictureBox1.Height;
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub SetStartGraphWidth ", ex.Message);
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private bool IsBetweenCurrentDay()
	{
		bool result = false;
		try
		{
			if (!string.IsNullOrEmpty(this.SymbolList))
			{
				if (this.TypeMode == enumMode.Between)
				{
					if (FormatUtil.Isdatetime(this.strCurDate) && FormatUtil.Isdatetime(this.strEndDate))
					{
						if (Convert.ToDateTime(this.strEndDate) >= Convert.ToDateTime(this.strCurDate))
						{
							result = true;
						}
					}
				}
				else
				{
					result = true;
				}
			}
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Function IsBetweenCurrentDay ", ex.Message);
			}
		}
		return result;
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void LoadControl()
	{
		try
		{
			if (!this.bLoaded)
			{
				this.SetGraphHeightWidth();
				this.SetBgColor();
				this.PictureBox1.Invalidate();
			}
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub wcVolumeAtPrice_Load ", ex.Message);
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void SetBgColor()
	{
		try
		{
			this.PictureBox1.BackColor = this.oGraphPanel.m_BgColor;
			this.BackColor = this.oGraphPanel.m_BgColor;
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub SetBgColor", ex.Message);
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private bool CanUseThisPermission()
	{
		bool flag = false;
		try
		{
			if (this.oGraphPanel != null)
			{
				if (this.TypeMode == enumMode.Previous)
				{
					flag = true;
				}
				else
				{
					if (this.oGraphPanel.ActiveType.Permission == enumPermission.Usable)
					{
						flag = true;
					}
				}
			}
			if (this.bLoaded && !flag)
			{
				this.PictureBox1.Invalidate();
			}
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub CanUseThisPermission ", ex.Message);
			}
		}
		return flag;
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void wcVolumeAtPrice_Load(object sender, EventArgs e)
	{
		this.LoadControl();
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void InitData(string Symbol, double Prior, double LastPrice, double Ceiling, double Floor)
	{
		try
		{
			this.oGraphPanel.Clear();
			this.oGraphPanel.SymbolList = Symbol;
			this.oGraphPanel.Prior = Prior;
			this.oGraphPanel.Last = LastPrice;
			this.oGraphPanel.Ceiling = Ceiling;
			this.oGraphPanel.Floor = Floor;
			this.bLoaded = true;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void InitData()
	{
		try
		{
			this.oGraphPanel.Clear();
			this.oGraphPanel.SymbolList = string.Empty;
			this.oGraphPanel.Prior = 0.0;
			this.oGraphPanel.Last = 0.0;
			this.oGraphPanel.Ceiling = 0.0;
			this.oGraphPanel.Floor = 0.0;
			this.bLoaded = true;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void InputData(double Price, long AccVolume, long BuyVolume, long SellVolume)
	{
		this.oGraphPanel.Add(Price, AccVolume, BuyVolume, SellVolume);
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void InputData(string stock, double value, double profit)
	{
		this.oGraphPanel.Add(stock, value, profit);
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void UpdateData(double Price, long AccVolume, long BuyVolume, long SellVolume)
	{
		this.oGraphPanel.Update(Price, AccVolume, BuyVolume, SellVolume);
		this.PictureBox1.Invalidate();
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void EndUpdate()
	{
		this.PictureBox1.Invalidate();
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void Sort()
	{
		this.oGraphPanel.Sort();
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void PictureBox1_Paint(object sender, PaintEventArgs e)
	{
		try
		{
			if (this.bLoaded && this.oGraphPanel != null)
			{
				if (this._mode == 0)
				{
					this.oGraphPanel.Draw(e.Graphics);
				}
				else
				{
					this.oGraphPanel.DrawByStock(e.Graphics);
				}
			}
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub PictureBox1_Paint ", ex.Message);
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void PictureBox1_Resize(object sender, EventArgs e)
	{
		try
		{
			this.oGraphPanel.Width = this.PictureBox1.Width;
			this.oGraphPanel.Height = this.PictureBox1.Height;
			if (this.bLoaded)
			{
				this.PictureBox1.Invalidate();
			}
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub PictureBox1_Resize ", ex.Message);
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
	{
		try
		{
			if (this._Activated != null)
			{
				this._Activated();
			}
			if (this.bLoaded)
			{
				if (e.Button == MouseButtons.Right)
				{
					if (!string.IsNullOrEmpty(this.oGraphPanel.strSymbolList))
					{
						if (this._OpenWith != null)
						{
							this._OpenWith(this.oGraphPanel.strSymbolList);
						}
					}
					else
					{
						if (this._OpenWith != null)
						{
							this._OpenWith("");
						}
					}
				}
				if (e.Button == MouseButtons.Left)
				{
					if (!string.IsNullOrEmpty(this.oGraphPanel.strSymbolList))
					{
						Color color = default(Color);
						color = this.oGraphPanel.GetCompareColor(this.oGraphPanel.Prior, this.oGraphPanel.Last);
						if (this._Drag != null)
						{
							this._Drag(this.oGraphPanel.strSymbolList, color);
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub PictureBox1_MouseDown ", ex.Message);
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public ucVolumeAtPrice()
	{
		base.Load += new EventHandler(this.wcVolumeAtPrice_Load);
		try
		{
			this.InitializeComponent();
		}
		catch (Exception ex)
		{
			if (this._SendErrorMsg != null)
			{
				this._SendErrorMsg("Sub New ", ex.Message);
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void InitializeComponent()
	{
		this.PictureBox1 = new PictureBox();
		((ISupportInitialize)this.PictureBox1).BeginInit();
		base.SuspendLayout();
		this.PictureBox1.Dock = DockStyle.Fill;
		this.PictureBox1.Location = new Point(0, 0);
		this.PictureBox1.Name = "PictureBox1";
		this.PictureBox1.Size = new Size(150, 150);
		this.PictureBox1.TabIndex = 0;
		this.PictureBox1.TabStop = false;
		this.PictureBox1.Resize += new EventHandler(this.PictureBox1_Resize);
		this.PictureBox1.MouseDown += new MouseEventHandler(this.PictureBox1_MouseDown);
		this.PictureBox1.Paint += new PaintEventHandler(this.PictureBox1_Paint);
		base.Controls.Add(this.PictureBox1);
		base.Name = "ucVolumeAtPrice";
		((ISupportInitialize)this.PictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}
