using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class BU10MessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private int group;
		private int commodity;
		private decimal stepSize;
		private int priceQuotFactor;
		private static StringBuilder lsString = new StringBuilder();
		private string[] Arr;
		public int Group
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.group;
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
		public decimal StepSize
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.stepSize;
			}
		}
		public int PriceQuotFactor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.priceQuotFactor;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "BU10";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BU10MessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BU10MessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int group, int commodity, decimal stepsize, int priceQuotFactor)
		{
			try
			{
				if (BU10MessageTFEX.lsString.Length > 0)
				{
					BU10MessageTFEX.lsString.Remove(0, BU10MessageTFEX.lsString.Length);
				}
				BU10MessageTFEX.lsString.Append("BU10 ");
				BU10MessageTFEX.lsString.Append(group);
				BU10MessageTFEX.lsString.Append(';');
				BU10MessageTFEX.lsString.Append(commodity);
				BU10MessageTFEX.lsString.Append(';');
				BU10MessageTFEX.lsString.Append(stepsize);
				BU10MessageTFEX.lsString.Append(';');
				BU10MessageTFEX.lsString.Append(priceQuotFactor);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return BU10MessageTFEX.lsString.ToString();
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
				int.TryParse(this.Arr[0], out this.group);
				int.TryParse(this.Arr[1], out this.commodity);
				decimal.TryParse(this.Arr[2], out this.stepSize);
				int.TryParse(this.Arr[3], out this.priceQuotFactor);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
