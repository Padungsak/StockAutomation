using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Information
{
	public class StockList
	{
		public class StockInformation
		{
			private int number = 0;
			private string symbol = string.Empty;
			private int boardLot = 0;
			private decimal ceiling = 0m;
			private decimal floor = 0m;
			private string displayFlag = string.Empty;
			private string marketId = string.Empty;
			private decimal priorPrice = 0m;
			private int sectorNumber = 0;
			private string securityType = string.Empty;
			private bool isOddLot = false;
			private decimal lastPrice = 0m;
			private decimal highPrice = 0m;
			private decimal lowPrice = 0m;
			private string bidPrice1 = string.Empty;
			private string offerPrice1 = string.Empty;
			private string stockBoard = string.Empty;
			private bool isCheckSpread = true;
			public int Number
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.number;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.number = value;
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
			public int BoardLot
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.boardLot;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.boardLot = value;
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
			public string DisplayFlag
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.displayFlag;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.displayFlag = value;
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
			public decimal PriorPrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.priorPrice;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.priorPrice = value;
				}
			}
			public int SectorNumber
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.sectorNumber;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.sectorNumber = value;
				}
			}
			public string SecurityType
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.securityType;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.securityType = value;
					this.SetStockBoard();
				}
			}
			public bool IsOddLot
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.isOddLot;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.isOddLot = value;
				}
			}
			public decimal LastSalePrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.lastPrice;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.lastPrice = value;
					if (value > this.highPrice)
					{
						this.highPrice = value;
					}
					if (this.lowPrice == 0m || value < this.lowPrice)
					{
						this.lowPrice = value;
					}
				}
			}
			public decimal HighPrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.highPrice;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.highPrice = value;
				}
			}
			public decimal LowPrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.lowPrice;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.lowPrice = value;
				}
			}
			public decimal ChangePrice
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					decimal result;
					if (this.lastPrice > 0m && this.priorPrice > 0m)
					{
						result = this.lastPrice - this.priorPrice;
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
					if (this.lastPrice > 0m && this.priorPrice > 0m)
					{
						result = Math.Round((this.lastPrice - this.priorPrice) / this.priorPrice * 100m, 4);
					}
					else
					{
						result = 0m;
					}
					return result;
				}
			}
			public string BidPrice1
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
			public string OfferPrice1
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
			public string StockBoard
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.stockBoard;
				}
			}
			public bool IsCheckSpread
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.isCheckSpread;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.isCheckSpread = value;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			private void SetStockBoard()
			{
				string text = this.securityType;
				switch (text)
				{
				case "S":
				case "U":
				case "M":
				case "D":
				case "C":
				case "P":
				case "W":
				case "J":
				case "G":
				case "R":
				case "I":
				case "X":
				case "V":
				case "O":
					this.stockBoard = "M";
					return;
				}
				this.stockBoard = "F";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public StockInformation()
			{
			}
		}
		private Dictionary<int, StockList.StockInformation> items = new Dictionary<int, StockList.StockInformation>();
		private Dictionary<string, int> itemsName = new Dictionary<string, int>();
		public Dictionary<int, StockList.StockInformation> Items
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.items;
			}
		}
		public StockList.StockInformation this[int stockNumber]
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StockList.StockInformation result;
				if (this.items.ContainsKey(stockNumber))
				{
					result = this.items[stockNumber];
				}
				else
				{
					result = new StockList.StockInformation
					{
						Number = -1,
						Symbol = "#NONE"
					};
				}
				return result;
			}
		}
		public StockList.StockInformation this[string stockSymbol]
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StockList.StockInformation result;
				if (this.itemsName.ContainsKey(stockSymbol))
				{
					result = this.items[this.itemsName[stockSymbol]];
				}
				else
				{
					result = new StockList.StockInformation
					{
						Number = -1,
						Symbol = "#NONE"
					};
				}
				return result;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void AddItem(StockList.StockInformation stockInfo)
		{
			try
			{
				if (!this.items.ContainsKey(stockInfo.Number))
				{
					this.items.Add(stockInfo.Number, stockInfo);
				}
				if (!this.itemsName.ContainsKey(stockInfo.Symbol))
				{
					this.itemsName.Add(stockInfo.Symbol, stockInfo.Number);
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
			if (this.items != null)
			{
				this.items.Clear();
			}
			if (this.itemsName != null)
			{
				this.itemsName.Clear();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Dispose()
		{
			this.ResetData();
			this.items = null;
			this.itemsName = null;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StockList()
		{
		}
	}
}
