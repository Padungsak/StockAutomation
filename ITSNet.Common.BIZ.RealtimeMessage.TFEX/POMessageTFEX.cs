using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class POMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string sec = string.Empty;
		private decimal equilibrium_price_I;
		private int equilibrium_quantity_i;
		private decimal best_bid_premium_i;
		private decimal best_ask_premium_i;
		private int best_bid_quantity_i;
		private int best_ask_quantity_i;
		private decimal price;
		private static StringBuilder lsString = new StringBuilder();
		private string[] Arr;
		public string Sec
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sec;
			}
		}
		public decimal Equilibrium_price_I
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.equilibrium_price_I;
			}
		}
		public int Equilibrium_quantity_i
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.equilibrium_quantity_i;
			}
		}
		public decimal Best_bid_premium_i
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.best_bid_premium_i;
			}
		}
		public decimal Best_ask_premium_i
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.best_ask_premium_i;
			}
		}
		public int Best_bid_quantity_i
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.best_bid_quantity_i;
			}
		}
		public int Best_ask_quantity_i
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.best_ask_quantity_i;
			}
		}
		public decimal Price
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "PO";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public POMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public POMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string sec, decimal projectedPrice, int bidQty, decimal bidPrice, int askQty, decimal askPrice)
		{
			try
			{
				if (POMessageTFEX.lsString.Length > 0)
				{
					POMessageTFEX.lsString.Remove(0, POMessageTFEX.lsString.Length);
				}
				POMessageTFEX.lsString.Append("PO  ");
				POMessageTFEX.lsString.Append(sec);
				POMessageTFEX.lsString.Append(';');
				POMessageTFEX.lsString.Append(projectedPrice);
				POMessageTFEX.lsString.Append(';');
				POMessageTFEX.lsString.Append(bidQty);
				POMessageTFEX.lsString.Append(';');
				POMessageTFEX.lsString.Append(bidPrice);
				POMessageTFEX.lsString.Append(';');
				POMessageTFEX.lsString.Append(askQty);
				POMessageTFEX.lsString.Append(';');
				POMessageTFEX.lsString.Append(askPrice);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return POMessageTFEX.lsString.ToString();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				message = message.Substring(4);
				this.Arr = message.Split(new char[]
				{
					';'
				});
				this.sec = this.Arr[0];
				decimal.TryParse(this.Arr[1], out this.equilibrium_price_I);
				int.TryParse(this.Arr[2], out this.best_bid_quantity_i);
				decimal.TryParse(this.Arr[3], out this.best_bid_premium_i);
				int.TryParse(this.Arr[4], out this.best_ask_quantity_i);
				decimal.TryParse(this.Arr[5], out this.best_ask_premium_i);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
