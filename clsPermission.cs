using System;
using System.Runtime.CompilerServices;
public class clsPermission
{
	private enumDisplayBuySell eDisplayBuySell = enumDisplayBuySell.Yes;
	private enumPermission ePermission = enumPermission.Visible;
	private double dblHistoricalDay = 30.0;
	private string strWordingType;
	private string strVolType;
	public string WordingType
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.strWordingType;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.strWordingType != value)
			{
				this.strWordingType = value;
			}
		}
	}
	public string VolType
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.strVolType;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.strVolType != value)
			{
				this.strVolType = value;
			}
		}
	}
	public double HistoricalDay
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblHistoricalDay;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.dblHistoricalDay != value)
			{
				this.dblHistoricalDay = value;
			}
		}
	}
	public enumPermission Permission
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.ePermission;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.ePermission != value)
			{
				this.ePermission = value;
			}
		}
	}
	public enumDisplayBuySell DisplayBuySell
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.eDisplayBuySell;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			if (this.eDisplayBuySell != value)
			{
				this.eDisplayBuySell = value;
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public clsPermission()
	{
	}
}
