using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class CoreBranchMessageService
	{
		private const int ROUTE_SIZE = 6;
		private const int BRANCH_SIZE = 2;
		private const int MESSAGE_TYPE_SIZE = 2;
		public const string SUCCESS_MESSAGE = "9X";
		public const string LOCAL_MESSAGE = "1X";
		public const string REPLY_MESSAGE = "9R";
		public const string SET_RX_MESSAGE = "RX";
		private string messageType;
		private string branchRoute;
		private string body;
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.messageType = value;
			}
		}
		public string BranchRoute
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.branchRoute;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.branchRoute = value;
			}
		}
		public string Body
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.body;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.body = value;
			}
		}
		public string LastestBranch
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.branchRoute.Trim() != string.Empty)
				{
					return this.branchRoute.Substring(0, 2);
				}
				return string.Empty;
			}
		}
		public string OwnerBranchID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.branchRoute.Trim() != string.Empty)
				{
					return this.branchRoute.Substring(this.branchRoute.Length - 2);
				}
				return string.Empty;
			}
		}
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageType.PadRight(2, ' ') + this.branchRoute.PadRight(6, ' ') + this.body;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DecreaseRoute()
		{
			if (this.branchRoute.Length >= 2)
			{
				this.branchRoute = this.branchRoute.Substring(2);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool UnPack(string message)
		{
			bool result;
			try
			{
				this.messageType = string.Empty;
				this.branchRoute = string.Empty;
				this.body = string.Empty;
				this.messageType = message.Substring(0, 2);
				this.branchRoute = message.Substring(2, 6).Trim();
				this.body = message.Substring(8);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string messageType, string branchRoute, string body)
		{
			return messageType.PadRight(2, ' ') + branchRoute.PadRight(6, ' ') + body;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CoreBranchMessageService()
		{
		}
	}
}
