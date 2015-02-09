using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.Tunnel
{
	public class Unregister
	{
		private char spliter = ';';
		private string userId = string.Empty;
		private string sessionId = string.Empty;
		private int userType;
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
		public int UserType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.userType;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string userId, string sessionId, int userType, string filter)
		{
			return string.Concat(new object[]
			{
				"UR",
				userId,
				this.spliter,
				sessionId,
				this.spliter,
				userType
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
			int.TryParse(array[2], out this.userType);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Unregister()
		{
		}
	}
}
