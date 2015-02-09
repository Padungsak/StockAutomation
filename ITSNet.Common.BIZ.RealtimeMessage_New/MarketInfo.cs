using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class MarketInfo : IBroadcastMessage
	{
		private const char splieter = ';';
		private DateTime timeStamp = DateTime.MinValue;
		private string marketState = string.Empty;
		private int marketSession;
		public DateTime TimeStamp
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.timeStamp;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.timeStamp = value;
			}
		}
		public string MarketState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.marketState;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.marketState = value;
			}
		}
		public int MarketSession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.marketSession;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.marketSession = value;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "MT";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MarketInfo()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MarketInfo(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string marketState, int marketSession)
		{
			return string.Concat(new object[]
			{
				"MT",
				DateTime.Now.ToString("HH:mm:ss"),
				';',
				marketState,
				';',
				marketSession.ToString()
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				message = message.Substring(2);
				string[] array = message.Split(new char[]
				{
					';'
				});
				if (FormatUtil.Isdatetime(array[0]))
				{
					this.timeStamp = Convert.ToDateTime(array[0]);
				}
				this.marketState = array[1];
				int.TryParse(array[2], out this.marketSession);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
