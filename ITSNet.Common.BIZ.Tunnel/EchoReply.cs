using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.Tunnel
{
	public class EchoReply
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack()
		{
			return "ER";
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public EchoReply()
		{
		}
	}
}
