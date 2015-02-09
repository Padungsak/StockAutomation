using System;
using System.Runtime.CompilerServices;
public class clsVolumeItem
{
	private double dblVolume;
	private int intx;
	private int inty;
	public int y
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.inty;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.inty = value;
		}
	}
	public int x
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.intx;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.intx = value;
		}
	}
	public double Volume
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblVolume;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblVolume = value;
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public clsVolumeItem(int x, int y, double Vol)
	{
		this.x = x;
		this.y = y;
		this.Volume = Vol;
	}
}
