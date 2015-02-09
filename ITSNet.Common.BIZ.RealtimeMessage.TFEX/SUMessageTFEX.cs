using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class SUMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string sec = string.Empty;
		private string expdate = string.Empty;
		private decimal strikePrice;
		private decimal openPrice;
		private decimal highPrice;
		private decimal lowPrice;
		private int turnover;
		private decimal closePrice;
		private int openBalance;
		private int blockSize;
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
		public string Expdate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.expdate;
			}
		}
		public decimal StrikePrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.strikePrice;
			}
		}
		public decimal OpenPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.openPrice;
			}
		}
		public decimal HighPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.highPrice;
			}
		}
		public decimal LowPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lowPrice;
			}
		}
		public int Turnover
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.turnover;
			}
		}
		public decimal ClosePrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.closePrice;
			}
		}
		public int OpenBalance
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.openBalance;
			}
		}
		public int BlockSize
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.blockSize;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "SU";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SUMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SUMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string sec, decimal strikePrice, decimal openPrice, decimal highPrice, decimal lowPrice, int turnOver, decimal closePrice, int openBalance, int blockSize, string expDate)
		{
			try
			{
				if (SUMessageTFEX.lsString.Length > 0)
				{
					SUMessageTFEX.lsString.Remove(0, SUMessageTFEX.lsString.Length);
				}
				SUMessageTFEX.lsString.Append("SU  ");
				SUMessageTFEX.lsString.Append(sec);
				SUMessageTFEX.lsString.Append(';');
				SUMessageTFEX.lsString.Append(strikePrice);
				SUMessageTFEX.lsString.Append(';');
				SUMessageTFEX.lsString.Append(openPrice);
				SUMessageTFEX.lsString.Append(';');
				SUMessageTFEX.lsString.Append(highPrice);
				SUMessageTFEX.lsString.Append(';');
				SUMessageTFEX.lsString.Append(lowPrice);
				SUMessageTFEX.lsString.Append(';');
				SUMessageTFEX.lsString.Append(turnOver);
				SUMessageTFEX.lsString.Append(';');
				SUMessageTFEX.lsString.Append(closePrice);
				SUMessageTFEX.lsString.Append(';');
				SUMessageTFEX.lsString.Append(openBalance);
				SUMessageTFEX.lsString.Append(';');
				SUMessageTFEX.lsString.Append(blockSize);
				SUMessageTFEX.lsString.Append(';');
				SUMessageTFEX.lsString.Append(expDate);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return SUMessageTFEX.lsString.ToString();
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
				decimal.TryParse(this.Arr[1], out this.strikePrice);
				decimal.TryParse(this.Arr[2], out this.openPrice);
				decimal.TryParse(this.Arr[3], out this.highPrice);
				decimal.TryParse(this.Arr[4], out this.lowPrice);
				int.TryParse(this.Arr[5], out this.turnover);
				decimal.TryParse(this.Arr[6], out this.closePrice);
				int.TryParse(this.Arr[7], out this.openBalance);
				int.TryParse(this.Arr[8], out this.blockSize);
				this.expdate = this.Arr[9];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
