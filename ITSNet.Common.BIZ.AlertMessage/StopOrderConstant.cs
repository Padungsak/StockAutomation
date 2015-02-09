using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.AlertMessage
{
	public class StopOrderConstant
	{
		public const string STOP_ORDER_COMMAND = "SO";
		public const string CANCEL_STOP_ORDER_COMMAND = "TC";
		public const int OPERATOR_GREATER_EQUAL = 1;
		public const int OPERATOR_LESSER_EQUAL = 2;
		public const int OPERATOR_GREATER = 3;
		public const int OPERATOR_LESSER = 4;
		public const int FIELD_LASTPRICE = 1;
		public const int FIELD_BID = 2;
		public const int FIELD_OFFER = 3;
		public const int FIELD_SMA = 4;
		public const int FIELD_HHV = 5;
		public const int FIELD_LLV = 6;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StopOrderConstant()
		{
		}
	}
}
