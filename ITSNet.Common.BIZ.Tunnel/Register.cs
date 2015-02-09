using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.Tunnel
{
	public class Register
	{
		public const int CLIENT_USER = 1;
		public const int TUNNEL_USER = 2;
		public const int TRADER_USER = 3;
		public const string InnerUserSessionID = "-1";
		private char spliter = ';';
		private string userId = string.Empty;
		private string sessionId = string.Empty;
		private int userType;
		private string accountList = string.Empty;
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
		public string AccountList
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.accountList;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.accountList = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string userId, string sessionId, int userType, string accountList)
		{
			return string.Concat(new object[]
			{
				"RG",
				userId,
				this.spliter,
				sessionId,
				this.spliter,
				userType,
				this.spliter,
				accountList
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
			this.accountList = array[3];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Register()
		{
		}
	}
}
