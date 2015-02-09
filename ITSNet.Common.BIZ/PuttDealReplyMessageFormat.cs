using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class PuttDealReplyMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private long confirmNumber;
		private long dealID;
		private string clientIDBuyer;
		private string replyCode;
		private int subBrokerID;
		private string trusteeIDBuyer;
		private long brokerPortVolume;
		private long brokerClientVolume;
		private long brokerMutualFundVolume;
		private long brokerForeignVolume;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
			}
		}
		public long ConfirmNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.confirmNumber;
			}
		}
		public long DealID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.dealID;
			}
		}
		public string ClientIDBuyer
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.clientIDBuyer;
			}
		}
		public string ReplyCode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.replyCode;
			}
		}
		public int SubBrokerID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.subBrokerID;
			}
		}
		public string TrusteeIDBuyer
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.trusteeIDBuyer;
			}
		}
		public long BrokerPortVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.brokerPortVolume;
			}
		}
		public long BrokerClientVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.brokerClientVolume;
			}
		}
		public long BrokerMutualFundVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.brokerMutualFundVolume;
			}
		}
		public long BrokerForeignVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.brokerForeignVolume;
			}
		}
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.PuttDealReply.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.confirmNumber,
					this.spliterString,
					this.dealID,
					this.spliterString,
					this.clientIDBuyer,
					this.spliterString,
					this.replyCode,
					this.spliterString,
					this.subBrokerID,
					this.spliterString,
					this.trusteeIDBuyer,
					this.spliterString,
					this.brokerPortVolume,
					this.spliterString,
					this.brokerClientVolume,
					this.spliterString,
					this.brokerMutualFundVolume,
					this.spliterString,
					this.brokerForeignVolume
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, long confirmNumber, long dealID, string clientIDBuyer, string replyCode, int subBrokerID, string trusteeIDBuyer, long brokerPortVolume, long brokerClientVolume, long brokerMutualFundVolume, long brokerForeignVolume)
		{
			this.firm = firm;
			this.confirmNumber = confirmNumber;
			this.dealID = dealID;
			this.clientIDBuyer = clientIDBuyer;
			this.replyCode = replyCode;
			this.subBrokerID = subBrokerID;
			this.trusteeIDBuyer = trusteeIDBuyer;
			this.brokerPortVolume = brokerPortVolume;
			this.brokerClientVolume = brokerClientVolume;
			this.brokerMutualFundVolume = brokerMutualFundVolume;
			this.brokerForeignVolume = brokerForeignVolume;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			string[] msgArray = message.Split(new char[]
			{
				'|'
			});
			this.Unpack(msgArray);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string[] msgArray)
		{
			try
			{
				this.firm = Convert.ToInt32(msgArray[1]);
				this.confirmNumber = Convert.ToInt64(msgArray[2]);
				this.dealID = Convert.ToInt64(msgArray[3]);
				this.clientIDBuyer = msgArray[4];
				this.replyCode = msgArray[5];
				this.subBrokerID = Convert.ToInt32(msgArray[6]);
				this.trusteeIDBuyer = msgArray[7];
				this.brokerPortVolume = 0L;
				long.TryParse(msgArray[8], out this.brokerPortVolume);
				this.brokerClientVolume = 0L;
				long.TryParse(msgArray[9], out this.brokerClientVolume);
				this.brokerMutualFundVolume = 0L;
				long.TryParse(msgArray[10], out this.brokerMutualFundVolume);
				this.brokerForeignVolume = 0L;
				long.TryParse(msgArray[11], out this.brokerForeignVolume);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PuttDealReplyMessageFormat()
		{
		}
	}
}
