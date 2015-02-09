using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class OrderConstant
	{
		public const string ATO_PRICE = "ATO";
		public const string ATC_PRICE = "ATC";
		public const string MP_PRICE = "MP";
		public const string MO_PRICE = "MO";
		public const string ML_PRICE = "ML";
		public const string CREDIT_TYPE1_REGULAR = "1";
		public const string CREDIT_TYPE2_CREDIT_LINE = "2";
		public const string CREDIT_TYPE3_TOTAL_EXPOSURE = "3";
		public const string CASH_ACCOUNT_TYPE = "C";
		public const string CREDIT_BALANCE_ACCOUNT_TYPE = "B";
		public const string BUY_SIDE = "B";
		public const string SELL_SIDE = "S";
		public const string SHORT_SELL_SIDE = "H";
		public const string COVER_SHORT_SELL_SIDE = "C";
		public const string EXERCISE_SIDE = "*";
		public const string MAIN_BOARD = "M";
		public const string ODDLOT_BOARD = "O";
		public const string BIGLOT_BOARD = "B";
		public const string MARKET_START = "S";
		public const string MARKET_PRE_OPEN = "P";
		public const string MARKET_OPEN = "O";
		public const string MARKET_BREAK = "B";
		public const string MARKET_CALL = "M";
		public const string MARKET_RUN_OFF = "R";
		public const string MARKET_CLOSE = "C";
		public const string MARKET_HALT = "H";
		public const string INTERNET_SENDER = "I";
		public const string LOCATION_IN_BROKER = "B";
		public const string LOCATION_IN_HOME = "H";
		public const string INVALID_SESSION_KEY = "INVALID_SESSION_KEY";
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OrderConstant()
		{
		}
	}
}
