using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class TCFMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string seriesName = string.Empty;
		private decimal ceiling;
		private decimal floor;
		private decimal prevFixPrice;
		private static StringBuilder lsString = new StringBuilder();
		private string[] Arr;
		public string SeriesName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.seriesName;
			}
		}
		public decimal Ceiling
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ceiling;
			}
		}
		public decimal Floor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.floor;
			}
		}
		public decimal PrevFixPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.prevFixPrice;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "TCF";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TCFMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TCFMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string seriesName, decimal ceiling, decimal floor, decimal prevFixPrice)
		{
			try
			{
				if (TCFMessageTFEX.lsString.Length > 0)
				{
					TCFMessageTFEX.lsString.Remove(0, TCFMessageTFEX.lsString.Length);
				}
				TCFMessageTFEX.lsString.Append("TCF  ");
				TCFMessageTFEX.lsString.Append(seriesName);
				TCFMessageTFEX.lsString.Append(';');
				TCFMessageTFEX.lsString.Append(ceiling);
				TCFMessageTFEX.lsString.Append(';');
				TCFMessageTFEX.lsString.Append(floor);
				TCFMessageTFEX.lsString.Append(';');
				TCFMessageTFEX.lsString.Append(prevFixPrice);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return TCFMessageTFEX.lsString.ToString();
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
				this.seriesName = this.Arr[0];
				decimal.TryParse(this.Arr[1], out this.ceiling);
				decimal.TryParse(this.Arr[2], out this.floor);
				decimal.TryParse(this.Arr[3], out this.prevFixPrice);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
