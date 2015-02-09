using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Information
{
	internal class AccountInfo
	{
		public class ItemInfo
		{
			public string AccountType = string.Empty;
			public string CanTrade = "Y";
			public string PcFlag = string.Empty;
			public string Market = string.Empty;
			public string TraderId = string.Empty;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public ItemInfo()
			{
			}
		}
		private Dictionary<string, AccountInfo.ItemInfo> items;
		public string CurrentAccount = string.Empty;
		public string CurrentAccountType = string.Empty;
		public string CurrInternetUser = string.Empty;
		public string InternetUser = string.Empty;
		public string InternetMutualUser = string.Empty;
		public string UserInternetInBroker = "Y";
		public string UserLists = string.Empty;
		public string InternetUserTFEX = string.Empty;
		public decimal CurrentCommRate = 0m;
		public decimal CurrentTradingFee = 0m;
		public decimal CurrentClearingFee = 0m;
		internal Dictionary<string, AccountInfo.ItemInfo> Items
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.items;
			}
		}
		public decimal TotalCommAndFee
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.CurrentCommRate + this.CurrentTradingFee + this.CurrentClearingFee;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AccountInfo()
		{
			this.items = new Dictionary<string, AccountInfo.ItemInfo>();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool IsAccCanTrade(string account)
		{
			bool result;
			try
			{
				AccountInfo.ItemInfo itemInfo;
				if (this.items.TryGetValue(account, out itemInfo))
				{
					result = (itemInfo.CanTrade == "Y");
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			result = false;
			return result;
		}
	}
}
