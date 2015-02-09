using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.AlertMessage
{
	public class TechnicalAlertCommand
	{
		private const char spliter = ';';
		private string messageType = "CC";
		private string userId = string.Empty;
		private string stock = string.Empty;
		private string indicator = string.Empty;
		private string cycle = string.Empty;
		private int period;
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
		public string Stock
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.stock;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.stock = value;
			}
		}
		public string Indicator
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.indicator;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.indicator = value;
			}
		}
		public string Cycle
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.cycle;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.cycle = value;
			}
		}
		public int Period
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.period;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.period = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TechnicalAlertCommand()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string userId, string stock, string indicator, string cycle, int period)
		{
			return string.Concat(new object[]
			{
				"SC",
				userId,
				';',
				stock,
				';',
				indicator,
				';',
				cycle,
				';',
				period
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
				this.stock = array[1];
				this.indicator = array[2];
				this.cycle = array[3];
				int.TryParse(array[4], out this.period);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
