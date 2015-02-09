using System;
using System.Runtime.CompilerServices;
public class VolumeGraphItem
{
	private double dblPrice;
	private long dblAccVol;
	private long dblBuyVol;
	private long dblSellVol;
	private int dblLeft;
	private int dblRight;
	private double dblAtoCVol;
	public double AtoCVol
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblAtoCVol;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblAtoCVol = value;
		}
	}
	public int Left
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblLeft;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblLeft = value;
		}
	}
	public int Right
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblRight;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblRight = value;
		}
	}
	public double Price
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblPrice;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblPrice = value;
		}
	}
	public long AccVol
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblAccVol;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblAccVol = value;
		}
	}
	public long BuyVol
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblBuyVol;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblBuyVol = value;
		}
	}
	public long SellVol
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblSellVol;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblSellVol = value;
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public VolumeGraphItem()
	{
	}
}
