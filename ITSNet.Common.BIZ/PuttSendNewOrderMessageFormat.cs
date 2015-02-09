using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class PuttSendNewOrderMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private string stock;
		private long volume;
		private decimal price;
		private int firm;
		private int contraFirm;
		private string clientIDSeller;
		private string clientIDBuyer;
		private string trusteeIDSeller;
		private string trusteeIDBuyer;
		private string traderIDSeller;
		private string traderIDBuyer;
		private string entryID;
		private string sessionID;
		private string requestID;
		private long orderNumber;
		private bool isForceUpdate;
		public string Stock
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.stock;
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
		public decimal Price
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price;
			}
		}
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
			}
		}
		public int ContraFirm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.contraFirm;
			}
		}
		public string ClientIDSeller
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.clientIDSeller;
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
		public string TrusteeIDSeller
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.trusteeIDSeller;
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
		public string TraderIDSeller
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.traderIDSeller;
			}
		}
		public string TraderIDBuyer
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.traderIDBuyer;
			}
		}
		public string EntryID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.entryID;
			}
		}
		public string SessionID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sessionID;
			}
		}
		public string RequestID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.requestID;
			}
		}
		public long OrderNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.orderNumber = value;
			}
		}
		public bool IsForceUpdate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isForceUpdate;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isForceUpdate = value;
			}
		}
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.PuttSendNewOrder.ToString(),
					this.spliterString,
					this.stock,
					this.spliterString,
					this.volume,
					this.spliterString,
					this.price,
					this.spliterString,
					this.firm,
					this.spliterString,
					this.contraFirm,
					this.spliterString,
					this.clientIDSeller,
					this.spliterString,
					this.clientIDBuyer,
					this.spliterString,
					this.trusteeIDSeller,
					this.spliterString,
					this.trusteeIDBuyer,
					this.spliterString,
					this.traderIDSeller,
					this.spliterString,
					this.traderIDBuyer,
					this.spliterString,
					this.entryID,
					this.spliterString,
					this.sessionID,
					this.spliterString,
					this.requestID,
					this.spliterString,
					this.orderNumber,
					this.spliterString,
					this.isForceUpdate
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string stock, long volume, decimal price, int firm, int contraFirm, string clientIDSeller, string clientIDBuyer, string trusteeIDSeller, string trusteeIDBuyer, string traderIDSeller, string traderIDBuyer, string entryID, string sessionID, string requestID, long orderNumber, bool isForceUpdate)
		{
			this.stock = stock;
			this.volume = volume;
			this.price = price;
			this.firm = firm;
			this.contraFirm = contraFirm;
			this.clientIDSeller = clientIDSeller;
			this.clientIDBuyer = clientIDBuyer;
			this.trusteeIDSeller = trusteeIDSeller;
			this.trusteeIDBuyer = trusteeIDBuyer;
			this.traderIDSeller = traderIDSeller;
			this.traderIDBuyer = traderIDBuyer;
			this.entryID = entryID;
			this.sessionID = sessionID;
			this.requestID = requestID;
			this.orderNumber = orderNumber;
			this.isForceUpdate = isForceUpdate;
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
				this.stock = msgArray[1];
				this.volume = Convert.ToInt64(msgArray[2]);
				this.price = Convert.ToDecimal(msgArray[3]);
				this.firm = Convert.ToInt32(msgArray[4]);
				this.contraFirm = Convert.ToInt32(msgArray[5]);
				this.clientIDSeller = msgArray[6];
				this.clientIDBuyer = msgArray[7];
				this.trusteeIDSeller = msgArray[8];
				this.trusteeIDBuyer = msgArray[9];
				this.traderIDSeller = msgArray[10];
				this.traderIDBuyer = msgArray[11];
				this.entryID = msgArray[12];
				this.sessionID = msgArray[13];
				this.requestID = msgArray[14];
				this.orderNumber = Convert.ToInt64(msgArray[15]);
				this.isForceUpdate = Convert.ToBoolean(msgArray[16]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PuttSendNewOrderMessageFormat()
		{
		}
	}
}
