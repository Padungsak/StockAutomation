using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Information
{
	internal class UnderlyingInfo
	{
		public class UnderlyingList
		{
			private int orderBookId;
			private string symbol = string.Empty;
			public int OrderBookId
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.orderBookId;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.orderBookId = value;
				}
			}
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
			[MethodImpl(MethodImplOptions.NoInlining)]
			public UnderlyingList()
			{
			}
		}
		private Dictionary<int, UnderlyingInfo.UnderlyingList> items = new Dictionary<int, UnderlyingInfo.UnderlyingList>();
		private Dictionary<string, int> itemsKeySymbol = new Dictionary<string, int>();
		public Dictionary<int, UnderlyingInfo.UnderlyingList> Items
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.items;
			}
		}
		public Dictionary<string, int> ItemsKeySymbol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.itemsKeySymbol;
			}
		}
		public UnderlyingInfo.UnderlyingList this[string indexName]
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				UnderlyingInfo.UnderlyingList result;
				try
				{
					if (this.itemsKeySymbol.ContainsKey(indexName))
					{
						result = this[this.itemsKeySymbol[indexName]];
					}
					else
					{
						result = new UnderlyingInfo.UnderlyingList
						{
							Symbol = string.Empty
						};
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return result;
			}
		}
		public UnderlyingInfo.UnderlyingList this[int orderBookId]
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				UnderlyingInfo.UnderlyingList result;
				try
				{
					if (this.items.ContainsKey(orderBookId))
					{
						result = this.items[orderBookId];
					}
					else
					{
						result = new UnderlyingInfo.UnderlyingList
						{
							Symbol = string.Empty
						};
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return result;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void AddItem(UnderlyingInfo.UnderlyingList info)
		{
			try
			{
				if (!this.items.ContainsKey(info.OrderBookId))
				{
					this.items.Add(info.OrderBookId, info);
					this.itemsKeySymbol.Add(info.Symbol, info.OrderBookId);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ResetData()
		{
			this.items.Clear();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Dispose()
		{
			this.ResetData();
			this.items.Clear();
			this.items = null;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public UnderlyingInfo()
		{
		}
	}
}
