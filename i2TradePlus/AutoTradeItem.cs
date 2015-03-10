using System;
using System.Runtime.CompilerServices;

namespace i2TradePlus
{
	class AutoTradeItem
	{
        private int refNo;
        private int stockNumber;
        private int fieldType;
        private int operatorType;
        private decimal price;
        private string ordSide;
        private long ordVolume;
        private long ordPubvol;
        private decimal ordPrice;
        private string ordCondition;
        private string status;
        private int time;
        private int mtime;
        private int orderNumber;
        private string limit;

        public int RefNo
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.refNo;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.refNo = value;
            }
        }
        public int StockNumber
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.stockNumber;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.stockNumber = value;
            }
        }
        public int FieldType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.fieldType;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.fieldType = value;
            }
        }
        public int OperatorType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.operatorType;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.operatorType = value;
            }
        }
        public decimal Price
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.price;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.price = value;
            }
        }
        public string OrdSide
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ordSide;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.ordSide = value;
            }
        }
        public long OrdVolume
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ordVolume;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.ordVolume = value;
            }
        }
        public long OrdPubvol
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ordPubvol;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.ordPubvol = value;
            }
        }
        public decimal OrdPrice
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ordPrice;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.ordPrice = value;
            }
        }
        public string OrdCondition
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ordCondition;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.ordCondition = value;
            }
        }
        public string Status
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.status;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.status = value;
            }
        }
        public int Time
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.time;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.time = value;
            }
        }
        public int Mtime
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.mtime;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.mtime = value;
            }
        }
        public int OrderNumber
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.orderNumber;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.orderNumber = value;
            }
        }
        public string Limit
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.limit;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.limit = value;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
		public AutoTradeItem()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
        public AutoTradeItem(AutoTradeItem item)
		{
            this.refNo = item.refNo;
            this.stockNumber = item.stockNumber;
            this.fieldType = item.fieldType;
            this.operatorType = item.operatorType;
            this.price = item.price;
            this.ordSide = item.ordSide;
            this.ordVolume = item.ordVolume;
            this.ordPubvol = item.ordPubvol;
            this.ordPrice = item.ordPrice;
            this.ordCondition = item.ordCondition;
            this.status = item.status;
            this.time = item.time;
            this.mtime = item.mtime;
            this.orderNumber = item.orderNumber;
            this.limit = item.limit;   
		}
	}
}
