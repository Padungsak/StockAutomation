using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class OrderReplyCode
	{
		public const int NONE = 0;
		public const int SEND_COMPLETED_CODE = 1;
		public const int REQUEST_APPROVE_CODE = 3;
		public const int ERROR_CODE = -1;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OrderReplyCode()
		{
		}
	}
}
