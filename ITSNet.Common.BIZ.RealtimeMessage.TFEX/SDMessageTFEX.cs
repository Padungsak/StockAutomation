using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class SDMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string sec = string.Empty;
		private int price_quot_factor;
		private string start_date_s = string.Empty;
		private string endDate = string.Empty;
		private int country;
		private int market;
		private int group;
		private int underOrderBookId;
		private string lastdate = string.Empty;
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
		public int Price_quot_factor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price_quot_factor;
			}
		}
		public string Start_date_s
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.start_date_s;
			}
		}
		public string EndDate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.endDate;
			}
		}
		public int Country
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.country;
			}
		}
		public int Market
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.market;
			}
		}
		public int Group
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.group;
			}
		}
		public int UnderOrderBookId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.underOrderBookId;
			}
		}
		public string Lastdate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastdate;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "SD";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SDMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SDMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string sec, decimal priceQuotFactor, string startDate, string endDate, int country, int market, int group, int underOrderBookId, string lastdate)
		{
			try
			{
				if (SDMessageTFEX.lsString.Length > 0)
				{
					SDMessageTFEX.lsString.Remove(0, SDMessageTFEX.lsString.Length);
				}
				SDMessageTFEX.lsString.Append("SD  ");
				SDMessageTFEX.lsString.Append(sec);
				SDMessageTFEX.lsString.Append(';');
				SDMessageTFEX.lsString.Append(priceQuotFactor);
				SDMessageTFEX.lsString.Append(';');
				SDMessageTFEX.lsString.Append(startDate);
				SDMessageTFEX.lsString.Append(';');
				SDMessageTFEX.lsString.Append(endDate);
				SDMessageTFEX.lsString.Append(';');
				SDMessageTFEX.lsString.Append(country);
				SDMessageTFEX.lsString.Append(';');
				SDMessageTFEX.lsString.Append(market);
				SDMessageTFEX.lsString.Append(';');
				SDMessageTFEX.lsString.Append(group);
				SDMessageTFEX.lsString.Append(';');
				SDMessageTFEX.lsString.Append(underOrderBookId);
				SDMessageTFEX.lsString.Append(';');
				SDMessageTFEX.lsString.Append(lastdate);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return SDMessageTFEX.lsString.ToString();
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
				int.TryParse(this.Arr[1], out this.price_quot_factor);
				this.start_date_s = this.Arr[2];
				this.endDate = this.Arr[3];
				int.TryParse(this.Arr[4], out this.country);
				int.TryParse(this.Arr[5], out this.market);
				int.TryParse(this.Arr[6], out this.group);
				int.TryParse(this.Arr[7], out this.underOrderBookId);
				this.lastdate = this.Arr[8];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
