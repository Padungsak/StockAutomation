using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class SSMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string sec = string.Empty;
		private decimal opening_price_i;
		private decimal high_price_i;
		private decimal low_price_i;
		private decimal last_price_i;
		private int volume_u;
		private int turnover_u;
		private int open_balance_u;
		private int number_of_deals_u;
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
		public decimal Opening_price_i
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.opening_price_i;
			}
		}
		public decimal High_price_i
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.high_price_i;
			}
		}
		public decimal Low_price_i
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.low_price_i;
			}
		}
		public decimal Last_price_i
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.last_price_i;
			}
		}
		public int Volume_u
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume_u;
			}
		}
		public int Turnover_u
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.turnover_u;
			}
		}
		public int Open_balance_u
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.open_balance_u;
			}
		}
		public int Number_of_deals_u
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.number_of_deals_u;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "SS";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SSMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SSMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string sec, decimal openPrice, decimal highPrice, decimal lowPrice, decimal lastPrice, int volume, int turnOver, int openBalance, int deals)
		{
			try
			{
				if (SSMessageTFEX.lsString.Length > 0)
				{
					SSMessageTFEX.lsString.Remove(0, SSMessageTFEX.lsString.Length);
				}
				SSMessageTFEX.lsString.Append("SS  ");
				SSMessageTFEX.lsString.Append(sec);
				SSMessageTFEX.lsString.Append(';');
				SSMessageTFEX.lsString.Append(openPrice);
				SSMessageTFEX.lsString.Append(';');
				SSMessageTFEX.lsString.Append(highPrice);
				SSMessageTFEX.lsString.Append(';');
				SSMessageTFEX.lsString.Append(lowPrice);
				SSMessageTFEX.lsString.Append(';');
				SSMessageTFEX.lsString.Append(lastPrice);
				SSMessageTFEX.lsString.Append(';');
				SSMessageTFEX.lsString.Append(volume);
				SSMessageTFEX.lsString.Append(';');
				SSMessageTFEX.lsString.Append(turnOver);
				SSMessageTFEX.lsString.Append(';');
				SSMessageTFEX.lsString.Append(openBalance);
				SSMessageTFEX.lsString.Append(';');
				SSMessageTFEX.lsString.Append(deals);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return SSMessageTFEX.lsString.ToString();
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
				decimal.TryParse(this.Arr[1], out this.opening_price_i);
				decimal.TryParse(this.Arr[2], out this.high_price_i);
				decimal.TryParse(this.Arr[3], out this.low_price_i);
				decimal.TryParse(this.Arr[4], out this.last_price_i);
				int.TryParse(this.Arr[5], out this.volume_u);
				int.TryParse(this.Arr[6], out this.turnover_u);
				int.TryParse(this.Arr[7], out this.open_balance_u);
				int.TryParse(this.Arr[8], out this.number_of_deals_u);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
