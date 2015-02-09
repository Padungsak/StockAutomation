using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class SCMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private string marketState = string.Empty;
		private int marketSession;
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
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "SC";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SCMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SCMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string marketState, int marketSession)
		{
			return string.Concat(new object[]
			{
				"SC",
				marketState,
				';',
				marketSession.ToString()
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			message = message.Substring(2);
			string[] array = message.Split(new char[]
			{
				';'
			});
			this.marketState = array[0];
			this.marketSession = Convert.ToInt32(array[1]);
		}
	}
}
