using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class IUMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private int orderBookId;
		private string indxName = string.Empty;
		private decimal lastIndx;
		private decimal highIndx;
		private decimal lowIndx;
		private decimal chgPrevIndx;
		private decimal chgYesterdayIndx;
		private static StringBuilder lsString = new StringBuilder();
		private string[] Arr;
		public int OrderBookId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderBookId;
			}
		}
		public string IndxName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.indxName;
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
		public decimal ChgPrevIndx
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.chgPrevIndx;
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
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "IU";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IUMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IUMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string indexName, decimal lastIndex, decimal highIndex, decimal lowIndex, decimal chgPrevIndex, decimal chgYesterdayIndex, int commodity)
		{
			try
			{
				if (IUMessageTFEX.lsString.Length > 0)
				{
					IUMessageTFEX.lsString.Remove(0, IUMessageTFEX.lsString.Length);
				}
				IUMessageTFEX.lsString.Append("IU  ");
				IUMessageTFEX.lsString.Append(indexName);
				IUMessageTFEX.lsString.Append(';');
				IUMessageTFEX.lsString.Append(FormatUtil.PriceFormat(lastIndex));
				IUMessageTFEX.lsString.Append(';');
				IUMessageTFEX.lsString.Append(FormatUtil.PriceFormat(highIndex));
				IUMessageTFEX.lsString.Append(';');
				IUMessageTFEX.lsString.Append(FormatUtil.PriceFormat(lowIndex));
				IUMessageTFEX.lsString.Append(';');
				IUMessageTFEX.lsString.Append(FormatUtil.PriceFormat(chgPrevIndex));
				IUMessageTFEX.lsString.Append(';');
				IUMessageTFEX.lsString.Append(FormatUtil.PriceFormat(chgYesterdayIndex));
				IUMessageTFEX.lsString.Append(';');
				IUMessageTFEX.lsString.Append(commodity);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return IUMessageTFEX.lsString.ToString();
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
				decimal.TryParse(this.Arr[1], out this.lastIndx);
				decimal.TryParse(this.Arr[2], out this.highIndx);
				decimal.TryParse(this.Arr[3], out this.lowIndx);
				decimal.TryParse(this.Arr[4], out this.chgPrevIndx);
				decimal.TryParse(this.Arr[5], out this.chgPrevIndx);
				int.TryParse(this.Arr[6], out this.orderBookId);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
