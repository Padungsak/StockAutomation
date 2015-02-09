using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.TAX
{
	public class AutoTAX
	{
		public class Hello
		{
			public int Seq;
			public string Password = string.Empty;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack(int seq, string password)
			{
				return AutoTAX.HELLO_MESSAGE + seq.ToString().PadRight(10, ' ') + password + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void UnPack(string data)
			{
				try
				{
					int.TryParse(data.Substring(2, 10), out this.Seq);
					this.Password = data.Substring(12);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public Hello()
			{
			}
		}
		public class HelloReply
		{
			public int Seq;
			public string Password = string.Empty;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack(int seq)
			{
				return AutoTAX.HELLO_REPLY_MESSAGE + seq.ToString() + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void UnPack(string data)
			{
				try
				{
					int.TryParse(data.Substring(2), out this.Seq);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public HelloReply()
			{
			}
		}
		public class Confirm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack()
			{
				return AutoTAX.CONFIRM_MESSAGE + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public Confirm()
			{
			}
		}
		public class Data
		{
			public int Seq;
			public string Message = string.Empty;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack(int seq, string message)
			{
				return AutoTAX.DATA_MESSAGE + seq.ToString().PadRight(10, ' ') + message + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void UnPack(string data)
			{
				try
				{
					int.TryParse(data.Substring(2, 10), out this.Seq);
					this.Message = data.Substring(12);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public Data()
			{
			}
		}
		public class AssetFeed
		{
			public string Data = string.Empty;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack(string data)
			{
				return AutoTAX.BCFEED_MESSAGE + data + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void UnPack(string data)
			{
				try
				{
					this.Data = data.Substring(2);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public AssetFeed()
			{
			}
		}
		public class AssetFeedRequestRetrans
		{
			public int startSeq;
			public int endSeq;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack(int startSeq, int endSeq)
			{
				return AutoTAX.RETRAN_REQUEST + startSeq.ToString().PadRight(10, ' ') + endSeq.ToString().PadRight(10, ' ') + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void UnPack(string message)
			{
				try
				{
					int.TryParse(message.Substring(2, 10), out this.startSeq);
					int.TryParse(message.Substring(12, 10), out this.endSeq);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public AssetFeedRequestRetrans()
			{
			}
		}
		public class AssetFeedReplyRetrans
		{
			public int Seq;
			public string Data;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack(int seq, string data)
			{
				return AutoTAX.RETRAN_REPLY + seq.ToString().PadRight(8, ' ') + data + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void UnPack(string message)
			{
				try
				{
					int.TryParse(message.Substring(2, 8), out this.Seq);
					this.Data = message.Substring(10);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public AssetFeedReplyRetrans()
			{
			}
		}
		public class Echo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack()
			{
				return AutoTAX.ECHO_MESSAGE + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public Echo()
			{
			}
		}
		public class EchoReply
		{
			public int ClientRecvSeq;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack(int clientRecvSeq)
			{
				return AutoTAX.ECHO_REPLY + clientRecvSeq + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void UnpPck(string message)
			{
				try
				{
					int.TryParse(message.Substring(2), out this.ClientRecvSeq);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public EchoReply()
			{
			}
		}
		public class LeftOverData
		{
			public int Seq;
			public string Data;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack(int seq, string data)
			{
				return AutoTAX.LEFT_OVER_DATA_MESSAGE + seq.ToString().PadRight(10, ' ') + data + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void UnPack(string message)
			{
				try
				{
					int.TryParse(message.Substring(2, 10), out this.Seq);
					this.Data = message.Substring(12);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public LeftOverData()
			{
			}
		}
		public class LeftOverLastData
		{
			public int Seq;
			public string Data;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack(int seq, string data)
			{
				return AutoTAX.LEFT_OVER_LAST_DATA_MESSAGE + seq.ToString().PadRight(10, ' ') + data + "\r\n";
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public void UnPack(string message)
			{
				try
				{
					int.TryParse(message.Substring(2, 10), out this.Seq);
					this.Data = message.Substring(12);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public LeftOverLastData()
			{
			}
		}
		private const string ETX = "\r\n";
		public static string RETRAN_REQUEST = "FQ";
		public static string RETRAN_REPLY = "FR";
		public static string ECHO_REPLY = "ER";
		public static string DATA_MESSAGE = "DT";
		public static string HELLO_MESSAGE = "HL";
		public static string HELLO_REPLY_MESSAGE = "HR";
		public static string CONFIRM_MESSAGE = "CF";
		public static string ECHO_MESSAGE = "EC";
		public static string BCFEED_MESSAGE = "AS";
		public static string LEFT_OVER_DATA_MESSAGE = "LO";
		public static string LEFT_OVER_LAST_DATA_MESSAGE = "LL";
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AutoTAX()
		{
		}
	}
}
