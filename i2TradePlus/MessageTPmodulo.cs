using ITSNet.Common.BIZ;
using System;
using System.Runtime.CompilerServices;
namespace i2TradePlus
{
	internal class MessageTPmodulo
	{
		private int stockNumber = 0;
		private string side = string.Empty;
		private decimal price1 = 0m;
		private long volume1 = 0L;
		private decimal price2 = 0m;
		private long volume2 = 0L;
		private decimal price3 = 0m;
		private long volume3 = 0L;
		private decimal price4 = 0m;
		private long volume4 = 0L;
		private decimal price5 = 0m;
		private long volume5 = 0L;
		internal int StockNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.stockNumber;
			}
		}
		internal string Side
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.side;
			}
		}
		internal decimal Price1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price1;
			}
		}
		internal long Volume1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume1;
			}
		}
		internal decimal Price2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price2;
			}
		}
		internal long Volume2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume2;
			}
		}
		internal decimal Price3
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price3;
			}
		}
		internal long Volume3
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume3;
			}
		}
		internal decimal Price4
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price4;
			}
		}
		internal long Volume4
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume4;
			}
		}
		internal decimal Price5
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price5;
			}
		}
		internal long Volume5
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume5;
			}
		}
		internal string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "TP";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal MessageTPmodulo()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal MessageTPmodulo(string message)
		{
			this.Decode(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void Decode(string message)
		{
			try
			{
				this.stockNumber = (int)AutoTManager.Demod96int(message.Substring(2, 2));
				this.side = message.Substring(4, 1);
				this.price1 = AutoTManager.Demod96dot(message.Substring(6, 4));
				this.volume1 = AutoTManager.Demod96int(message.Substring(10, 5));
				this.price2 = AutoTManager.Demod96dot(message.Substring(15, 4));
				this.volume2 = AutoTManager.Demod96int(message.Substring(19, 5));
				this.price3 = AutoTManager.Demod96dot(message.Substring(24, 4));
				this.volume3 = AutoTManager.Demod96int(message.Substring(28, 5));
				this.price4 = AutoTManager.Demod96dot(message.Substring(33, 4));
				this.volume4 = AutoTManager.Demod96int(message.Substring(37, 5));
				this.price5 = AutoTManager.Demod96dot(message.Substring(42, 4));
				this.volume5 = AutoTManager.Demod96int(message.Substring(46, 5));
			}
			catch (Exception ex)
			{
				this.stockNumber = 0;
				this.side = string.Empty;
				this.price1 = 0m;
				this.volume1 = 0L;
				this.price2 = 0m;
				this.volume2 = 0L;
				this.price3 = 0m;
				this.volume3 = 0L;
				this.price4 = 0m;
				this.volume4 = 0L;
				this.price5 = 0m;
				this.volume5 = 0L;
				throw ex;
			}
		}
	}
}
