using System;
using System.Runtime.CompilerServices;
public class ItemByStock
{
	private string _stock;
	private double _value;
	private double _profit;
	public double Value
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this._value;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this._value = value;
		}
	}
	public double Profit
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this._profit;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this._profit = value;
		}
	}
	public string Stock
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this._stock;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this._stock = value;
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public ItemByStock()
	{
	}
}
