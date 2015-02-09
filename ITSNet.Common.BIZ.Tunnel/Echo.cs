using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.Tunnel
{
	public class Echo
	{
		private char spliter = ';';
		private string userId = string.Empty;
		private string sessionId = string.Empty;
		public string UserId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.userId;
			}
		}
		public string SessionId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sessionId;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string userId, string sessionId)
		{
			return string.Concat(new object[]
			{
				"EC",
				userId,
				this.spliter,
				sessionId
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			string[] array = message.Substring(2).Split(new char[]
			{
				this.spliter
			});
			this.userId = array[0];
			this.sessionId = array[1];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Echo()
		{
		}
	}
}
