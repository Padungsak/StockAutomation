using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class OrderTFEXInfoClient : IBroadcastMessage
	{
		private const char spliter = ';';
		private string messageType = "#T9I";
		private long orderNumber;
		private string position;
		private string account;
		private string side;
		private string series;
		private long volume;
		private string price;
		private long matchedVolume;
		private long publicVolume;
		private string status;
		private string orderTime;
		private string quote;
		private string orgMessageType = string.Empty;
		private string sendDate = string.Empty;
		private string orderType;
		private string rejectDescription = string.Empty;
		private long lastVolumeInCase;
		private string lastPriceInCase = string.Empty;
		private string[] msg;
		public long OrderNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderNumber;
			}
		}
		public string Position
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.position;
			}
		}
		public string Account
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.account;
			}
		}
		public string Side
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.side;
			}
		}
		public string Series
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.series;
			}
		}
		public long Volume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume;
			}
		}
		public string Price
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price;
			}
		}
		public long MatchedVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.matchedVolume;
			}
		}
		public long PublicVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.publicVolume;
			}
		}
		public string Status
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.status;
			}
		}
		public string OrderTime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderTime;
			}
		}
		public string Quote
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.quote;
			}
		}
		public string OriginalMessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orgMessageType;
			}
		}
		public string SendDate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sendDate;
			}
		}
		public string OrderType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderType;
			}
		}
		public string RejectDescription
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.rejectDescription;
			}
		}
		public long LastVolumeInCase
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastVolumeInCase;
			}
		}
		public string LastPriceInCase
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastPriceInCase;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageType;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OrderTFEXInfoClient()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OrderTFEXInfoClient(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string OrgMessagetype, string accountId, string orderNumber, string position, string side, string series, long volume, string price, long matchedVolume, long publicVolume, string status, string orderTime, string quote, string sendDate, string ordType, long lastVolumeInCase, string lastPriceInCase, string RejectDescription)
		{
			return string.Concat(new object[]
			{
				this.messageType,
				orderNumber,
				';',
				position,
				';',
				side,
				';',
				series,
				';',
				volume,
				';',
				price,
				';',
				matchedVolume,
				';',
				publicVolume,
				';',
				status,
				';',
				orderTime,
				';',
				quote,
				';',
				OrgMessagetype,
				';',
				accountId,
				';',
				sendDate,
				';',
				ordType,
				';',
				lastVolumeInCase,
				';',
				lastPriceInCase,
				';',
				RejectDescription
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				this.messageType = message.Substring(0, 4);
				this.msg = message.Substring(4).Split(new char[]
				{
					';'
				});
				long.TryParse(this.msg[0], out this.orderNumber);
				this.position = this.msg[1];
				this.side = this.msg[2];
				this.series = this.msg[3];
				long.TryParse(this.msg[4], out this.volume);
				this.price = this.msg[5];
				long.TryParse(this.msg[6], out this.matchedVolume);
				long.TryParse(this.msg[7], out this.publicVolume);
				this.status = this.msg[8];
				this.orderTime = this.msg[9];
				this.quote = this.msg[10];
				this.orgMessageType = this.msg[11];
				this.account = this.msg[12];
				this.sendDate = this.msg[13];
				this.orderType = this.msg[14];
				long.TryParse(this.msg[15], out this.lastVolumeInCase);
				this.lastPriceInCase = this.msg[16];
				this.rejectDescription = this.msg[17];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
