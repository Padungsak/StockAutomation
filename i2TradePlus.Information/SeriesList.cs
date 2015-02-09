using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Information
{
	public class SeriesList
	{
		public class SeriesInformation
		{
			private int orderBookId;
			private int underOrderBookId;
			private string symbol = string.Empty;
			private string seriesType = string.Empty;
			private decimal contractSize = 0m;
			private int numOfDec = 0;
			private decimal ceiling = 0m;
			private decimal floor = 0m;
			private int marketCode = 0;
			private int group = 0;
			private decimal prevfixPrice = 0m;
			private decimal fixPrice = 0m;
			private string expireDate = null;
			private decimal bidPrice1;
			private decimal offerPrice1;
			private int openInt;
			private decimal tickSize;
			private decimal strikPrice;
			private decimal lastSalePrice;
			private string marketId = string.Empty;
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
			public int UnderOrderBookId
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.underOrderBookId;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.underOrderBookId = value;
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
			public string SeriesType
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.seriesType;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.seriesType = value;
				}
			}
			public decimal ContractSize
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.contractSize;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.contractSize = value;
				}
			}
			public int NumOfDec
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.numOfDec;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.numOfDec = value;
				}
			}
			public decimal Ceiling
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.ceiling;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.ceiling = value;
				}
			}
			public decimal Floor
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.floor;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.floor = value;
				}
			}
			public int MarketCode
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.marketCode;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.marketCode = value;
				}
			}
			public int Group
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.group;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.group = value;
				}
			}
			public decimal PrevFixPrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.prevfixPrice;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.prevfixPrice = value;
				}
			}
			public decimal FixPrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.fixPrice;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.fixPrice = value;
				}
			}
			public string ExpireDate
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.expireDate;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.expireDate = value;
				}
			}
			public decimal BidPrice1
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.bidPrice1;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.bidPrice1 = value;
				}
			}
			public decimal OfferPrice1
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.offerPrice1;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.offerPrice1 = value;
				}
			}
			public int OpenInt
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.openInt;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.openInt = value;
				}
			}
			public decimal TickSize
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.tickSize;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.tickSize = value;
				}
			}
			public decimal StrikPrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.strikPrice;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.strikPrice = value;
				}
			}
			public decimal LastSalePrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.lastSalePrice;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.lastSalePrice = value;
				}
			}
			public decimal ChangePrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					decimal result;
					if (this.lastSalePrice > 0m && this.prevfixPrice > 0m)
					{
						result = this.lastSalePrice - this.prevfixPrice;
					}
					else
					{
						result = 0m;
					}
					return result;
				}
			}
			public decimal ChangePricePct
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					decimal result;
					if (this.lastSalePrice > 0m && this.prevfixPrice > 0m)
					{
						result = Math.Round((this.lastSalePrice - this.prevfixPrice) / this.prevfixPrice * 100m, 4);
					}
					else
					{
						result = 0m;
					}
					return result;
				}
			}
			public string MarketId
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.marketId;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.marketId = value;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public SeriesInformation()
			{
			}
		}
		private Dictionary<int, SeriesList.SeriesInformation> items = new Dictionary<int, SeriesList.SeriesInformation>();
		private Dictionary<string, int> itemsKeySymbol = new Dictionary<string, int>();
		public Dictionary<int, SeriesList.SeriesInformation> Items
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
		public SeriesList.SeriesInformation this[string seriesName]
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				SeriesList.SeriesInformation result;
				try
				{
					if (this.itemsKeySymbol.ContainsKey(seriesName))
					{
						result = this[this.itemsKeySymbol[seriesName]];
					}
					else
					{
						result = new SeriesList.SeriesInformation
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
		public SeriesList.SeriesInformation this[int orderBookId]
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				SeriesList.SeriesInformation result;
				try
				{
					if (this.items.ContainsKey(orderBookId))
					{
						result = this.items[orderBookId];
					}
					else
					{
						result = new SeriesList.SeriesInformation
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
		public void AddItem(SeriesList.SeriesInformation info)
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
		public SeriesList()
		{
		}
	}
}
