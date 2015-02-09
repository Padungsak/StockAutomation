using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class STMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string marketState = string.Empty;
		private int marketSession;
		private int marketCode;
		private static StringBuilder lsString = new StringBuilder();
		private string[] Arr;
		public string MarketState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.marketState;
			}
		}
		public int MarketSession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.marketSession;
			}
		}
		public int MarketCode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.marketCode;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "ST";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public STMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public STMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string marketState, int marketSession, int marketCode)
		{
			try
			{
				if (STMessageTFEX.lsString.Length > 0)
				{
					STMessageTFEX.lsString.Remove(0, STMessageTFEX.lsString.Length);
				}
				STMessageTFEX.lsString.Append("ST  ");
				STMessageTFEX.lsString.Append(marketState);
				STMessageTFEX.lsString.Append(';');
				STMessageTFEX.lsString.Append(marketSession);
				STMessageTFEX.lsString.Append(';');
				STMessageTFEX.lsString.Append(marketCode);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return STMessageTFEX.lsString.ToString();
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
				this.marketState = this.Arr[0];
				int.TryParse(this.Arr[1], out this.marketSession);
				int.TryParse(this.Arr[2], out this.marketCode);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
