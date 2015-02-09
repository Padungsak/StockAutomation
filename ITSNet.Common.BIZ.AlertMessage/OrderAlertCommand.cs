using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.AlertMessage
{
	public class OrderAlertCommand
	{
		private const char spliter = ';';
		private string messageType = "OC";
		private string userId = string.Empty;
		private string account = string.Empty;
		private bool isAlert;
		public string UserId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.userId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.userId = value;
			}
		}
		public string Account
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.account;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.account = value;
			}
		}
		public bool IsAlert
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isAlert;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isAlert = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string userId, string account, bool isAlert)
		{
			return string.Concat(new object[]
			{
				"OC",
				userId,
				';',
				account,
				';',
				isAlert
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				this.messageType = message.Substring(0, 2);
				string[] array = message.Substring(2).Split(new char[]
				{
					';'
				});
				this.userId = array[0];
				this.account = array[1];
				bool.TryParse(array[2], out this.isAlert);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OrderAlertCommand()
		{
		}
	}
}
