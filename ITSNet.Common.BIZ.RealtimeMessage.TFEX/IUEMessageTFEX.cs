using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class IUEMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string indxName = string.Empty;
		private decimal totVal;
		private decimal lastIndx;
		private decimal highIndx;
		private decimal lowIndx;
		private decimal chgYesterdayIndx;
		private int commodity;
		private string mktID = string.Empty;
		private static StringBuilder lsString = new StringBuilder();
		private string[] Arr;
		public string IndxName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.indxName;
			}
		}
		public decimal TotVal
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totVal;
			}
		}
		public decimal LastIndx
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastIndx;
			}
		}
		public decimal HighIndx
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.highIndx;
			}
		}
		public decimal LowIndx
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lowIndx;
			}
		}
		public decimal ChgYesterdayIndx
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.chgYesterdayIndx;
			}
		}
		public int Commodity
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.commodity;
			}
		}
		public string MktID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.mktID;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "IUE";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IUEMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IUEMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string indexName, decimal totVal, decimal lastIndex, decimal highIndex, decimal lowIndex, decimal chgYesterdayIndex, int commodity, string mktID)
		{
			try
			{
				if (IUEMessageTFEX.lsString.Length > 0)
				{
					IUEMessageTFEX.lsString.Remove(0, IUEMessageTFEX.lsString.Length);
				}
				IUEMessageTFEX.lsString.Append("IUE ");
				IUEMessageTFEX.lsString.Append(indexName);
				IUEMessageTFEX.lsString.Append(';');
				IUEMessageTFEX.lsString.Append(FormatUtil.PriceFormat(totVal));
				IUEMessageTFEX.lsString.Append(';');
				IUEMessageTFEX.lsString.Append(FormatUtil.PriceFormat(lastIndex));
				IUEMessageTFEX.lsString.Append(';');
				IUEMessageTFEX.lsString.Append(FormatUtil.PriceFormat(highIndex));
				IUEMessageTFEX.lsString.Append(';');
				IUEMessageTFEX.lsString.Append(FormatUtil.PriceFormat(lowIndex));
				IUEMessageTFEX.lsString.Append(';');
				IUEMessageTFEX.lsString.Append(FormatUtil.PriceFormat(chgYesterdayIndex));
				IUEMessageTFEX.lsString.Append(';');
				IUEMessageTFEX.lsString.Append(commodity);
				IUEMessageTFEX.lsString.Append(';');
				IUEMessageTFEX.lsString.Append(mktID);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return IUEMessageTFEX.lsString.ToString();
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
				this.indxName = this.Arr[0];
				decimal.TryParse(this.Arr[1], out this.totVal);
				decimal.TryParse(this.Arr[2], out this.lastIndx);
				decimal.TryParse(this.Arr[3], out this.highIndx);
				decimal.TryParse(this.Arr[4], out this.lowIndx);
				decimal.TryParse(this.Arr[5], out this.chgYesterdayIndx);
				int.TryParse(this.Arr[6], out this.commodity);
				this.mktID = this.Arr[7];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
