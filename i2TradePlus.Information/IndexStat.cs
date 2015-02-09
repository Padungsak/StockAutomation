using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Information
{
	public class IndexStat
	{
		public class IndexItem
		{
			private int number;
			private string type = string.Empty;
			private string symbol;
			private string fullName;
			private decimal prior;
			private decimal lastIndex;
			private decimal accValue;
			private decimal indexHigh;
			private decimal indexLow;
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
			public string Type
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.type;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.type = value;
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
			public string Fullname
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.fullName;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.fullName = value;
				}
			}
			public decimal Prior
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.prior;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.prior = value;
				}
			}
			public decimal LastIndex
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.lastIndex;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.lastIndex = value;
				}
			}
			public decimal AccValue
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.accValue;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.accValue = value;
				}
			}
			public decimal IndexHigh
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.indexHigh;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.indexHigh = value;
				}
			}
			public decimal IndexLow
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.indexLow;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.indexLow = value;
				}
			}
			public decimal IndexChg
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					decimal result;
					if (this.prior > 0m && this.lastIndex > 0m)
					{
						result = this.lastIndex - this.prior;
					}
					else
					{
						result = 0m;
					}
					return result;
				}
			}
			public decimal IndexChgPct
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					decimal result;
					if (this.prior > 0m && this.lastIndex > 0m)
					{
						result = (this.lastIndex - this.prior) / this.prior * 100m;
					}
					else
					{
						result = 0m;
					}
					return result;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public IndexItem()
			{
			}
		}
		private List<IndexStat.IndexItem> items = new List<IndexStat.IndexItem>();
		public List<IndexStat.IndexItem> Items
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.items;
			}
		}
		public IndexStat.IndexItem this[string symbol]
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.items.Find((IndexStat.IndexItem item) => item.Symbol == symbol);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IndexStat.IndexItem GetSector(int sectorNo)
		{
			return this.items.Find((IndexStat.IndexItem item) => item.Number == sectorNo && item.Type == "S");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IndexStat.IndexItem GetIndustry(int industryNo)
		{
			return this.items.Find((IndexStat.IndexItem item) => item.Number == industryNo && item.Type == "I");
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
			this.items = null;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IndexStat()
		{
		}
	}
}
