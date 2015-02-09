using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.AlertMessage
{
	public class AlertConstant
	{
		public const string STOCK_ALERT_COMMAND = "SC";
		public const string PORT_ALERT_COMMAND = "PC";
		public const string ORDER_ALERT_COMMAND = "OC";
		public const string MAIL_ALERT_COMMAND = "MC";
		public const string PORT_SUMMARY_ALERT_COMMAND = "SM";
		public const string TCHART_ALERT_COMMAND = "CC";
		public const string LS_AUTOTRADE = "LA";
		public const int OPERATOR_EQUAL = 1;
		public const int OPERATOR_GREATER_EQUAL = 2;
		public const int OPERATOR_LESS_THAN_EQUAL = 3;
		public const string ALERT_LASTPRICE_COND = "LAST";
		public const string ALERT_CHANGE_PCT_COND = "PCHG";
		public const string ALERT_PORT_PCHANGE_COND = "PFCHG";
		public const string ALERT_PORT_COST_COND = "PFCOST";
		public const string CMPR_NEW_COMMAND = "NC";
		public const string CMPR_CANCEL_COMMAND = "SC";
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AlertConstant()
		{
		}
	}
}
