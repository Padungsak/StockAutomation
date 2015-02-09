using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.Tunnel
{
	public class KickClient
	{
		private char spliter = ';';
		private string userId = string.Empty;
		public string UserId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.userId;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string userId)
		{
			return "KC" + userId;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			string[] array = message.Substring(2).Split(new char[]
			{
				this.spliter
			});
			this.userId = array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public KickClient()
		{
		}
	}
}
