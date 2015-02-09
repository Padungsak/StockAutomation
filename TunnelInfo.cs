using System;
using System.Runtime.CompilerServices;
internal class TunnelInfo
{
	private string hostIP = string.Empty;
	private int port = 0;
	private bool enableSSL = true;
	private bool isAlreadyStart = false;
	public string HostIP
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.hostIP;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.hostIP = value;
		}
	}
	public int Port
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.port;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.port = value;
		}
	}
	public bool EnableSSL
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.enableSSL;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.enableSSL = value;
		}
	}
	public bool IsAlreadyStart
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.isAlreadyStart;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.isAlreadyStart = value;
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public TunnelInfo()
	{
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public TunnelInfo(string hostIP, int port, bool enableSSL)
	{
		this.HostIP = hostIP;
		this.Port = port;
		this.EnableSSL = enableSSL;
	}
}
