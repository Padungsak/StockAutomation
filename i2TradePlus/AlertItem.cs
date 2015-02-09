using System;
using System.Runtime.CompilerServices;
namespace i2TradePlus
{
	public class AlertItem
	{
		public const int STOCK_ALERT_TYPE = 1;
		public const int EMPTY_PORT_ALERT_TYPE = 3;
		private string symbol;
		private bool isReaded = false;
		private DateTime alertTime = DateTime.MinValue;
		private string fieldCondition = null;
		private string operatorCondition = null;
		private string valueCondition = null;
		private string valueMessageRealtime = string.Empty;
		private int alertType = 0;
		public string Symbol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.symbol;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.symbol = value;
			}
		}
		public bool IsReaded
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isReaded;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isReaded = value;
			}
		}
		public DateTime AlertTime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.alertTime;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.alertTime = value;
			}
		}
		public string Field
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.fieldCondition;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.fieldCondition = value;
			}
		}
		public string Operator
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.operatorCondition;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.operatorCondition = value;
			}
		}
		public string Value
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.valueCondition;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.valueCondition = value;
			}
		}
		public string ValueMessageRealtime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.valueMessageRealtime;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.valueMessageRealtime = value;
			}
		}
		public int AlertType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.alertType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.alertType = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AlertItem()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AlertItem(AlertItem item)
		{
			this.symbol = item.symbol;
			this.isReaded = item.IsReaded;
			this.fieldCondition = item.Field;
			this.operatorCondition = item.Operator;
			this.valueCondition = item.Value;
			this.valueMessageRealtime = item.ValueMessageRealtime;
		}
	}
}
