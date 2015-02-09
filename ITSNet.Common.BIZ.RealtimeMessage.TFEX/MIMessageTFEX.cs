using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class MIMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private long futuresVol;
		private long futuresOI;
		private long optionsVol;
		private long optionsOI;
		private long totalVol;
		private long totalDeal;
		private long totalOI;
		private static StringBuilder lsString = new StringBuilder();
		private string[] Arr;
		public long FuturesVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.futuresVol;
			}
		}
		public long FuturesOI
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.futuresOI;
			}
		}
		public long OptionsVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.optionsVol;
			}
		}
		public long OptionsOI
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.optionsOI;
			}
		}
		public long TotalVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalVol;
			}
		}
		public long TotalDeal
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalDeal;
			}
		}
		public long TotalOI
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalOI;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "MI";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MIMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MIMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(long futuresVol, long futuresOI, long optionsVol, long OptionsOI, long totalVol, long totalDeal, long totalOI)
		{
			try
			{
				if (MIMessageTFEX.lsString.Length > 0)
				{
					MIMessageTFEX.lsString.Remove(0, MIMessageTFEX.lsString.Length);
				}
				MIMessageTFEX.lsString.Append("MI  ");
				MIMessageTFEX.lsString.Append(futuresVol);
				MIMessageTFEX.lsString.Append(';');
				MIMessageTFEX.lsString.Append(futuresOI);
				MIMessageTFEX.lsString.Append(';');
				MIMessageTFEX.lsString.Append(optionsVol);
				MIMessageTFEX.lsString.Append(';');
				MIMessageTFEX.lsString.Append(OptionsOI);
				MIMessageTFEX.lsString.Append(';');
				MIMessageTFEX.lsString.Append(totalVol);
				MIMessageTFEX.lsString.Append(';');
				MIMessageTFEX.lsString.Append(totalDeal);
				MIMessageTFEX.lsString.Append(';');
				MIMessageTFEX.lsString.Append(totalOI);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return MIMessageTFEX.lsString.ToString();
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
				long.TryParse(this.Arr[0], out this.futuresVol);
				long.TryParse(this.Arr[1], out this.futuresOI);
				long.TryParse(this.Arr[2], out this.optionsVol);
				long.TryParse(this.Arr[3], out this.optionsOI);
				long.TryParse(this.Arr[4], out this.totalVol);
				long.TryParse(this.Arr[5], out this.totalDeal);
				long.TryParse(this.Arr[6], out this.totalOI);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
