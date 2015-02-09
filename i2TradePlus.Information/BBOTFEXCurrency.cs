using System;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Information
{
	internal class BBOTFEXCurrency
	{
		private string currencyName = string.Empty;
		private decimal bidPrice = 0m;
		private decimal askPrice = 0m;
		private string lastDate = string.Empty;
		private string lastTime = string.Empty;
		public string CurrencyName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.currencyName;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.currencyName = value;
			}
		}
		public decimal BidPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.bidPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.bidPrice = value;
			}
		}
		public decimal AskPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.askPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.askPrice = value;
			}
		}
		public string LastDate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastDate;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.lastDate = value;
			}
		}
		public string LastTime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastTime;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.lastTime = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BBOTFEXCurrency()
		{
		}
	}
}
