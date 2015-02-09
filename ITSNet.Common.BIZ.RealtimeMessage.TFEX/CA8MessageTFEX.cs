using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class CA8MessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string sec = string.Empty;
		private decimal fixingPrice;
		private int decPrice = 2;
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
		public decimal FixingPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.fixingPrice;
			}
		}
		public int DecPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.decPrice;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "CA8";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CA8MessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CA8MessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string sec, decimal fixingPrice, int decPrice)
		{
			try
			{
				if (CA8MessageTFEX.lsString.Length > 0)
				{
					CA8MessageTFEX.lsString.Remove(0, CA8MessageTFEX.lsString.Length);
				}
				CA8MessageTFEX.lsString.Append("CA8 ");
				CA8MessageTFEX.lsString.Append(sec);
				CA8MessageTFEX.lsString.Append(';');
				CA8MessageTFEX.lsString.Append(fixingPrice);
				CA8MessageTFEX.lsString.Append(';');
				CA8MessageTFEX.lsString.Append(decPrice);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return CA8MessageTFEX.lsString.ToString();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				message = message.Substring(4).Trim();
				this.Arr = message.Split(new char[]
				{
					';'
				});
				this.sec = this.Arr[0];
				decimal.TryParse(this.Arr[1], out this.fixingPrice);
				int.TryParse(this.Arr[2], out this.decPrice);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
