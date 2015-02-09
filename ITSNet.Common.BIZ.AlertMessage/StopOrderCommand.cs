using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.AlertMessage
{
	public class StopOrderCommand
	{
		private const char spliter = ';';
		private string messageType = "SO";
		private int refNo;
		private int operatorType = -1;
		private int fieldType;
		private int checkPeriods;
		private decimal tmpPeriodPrice;
		private decimal checkPrice;
		private int stockNumber = -1;
		private string userId = string.Empty;
		private string kmSession = string.Empty;
		private string kmLocal = string.Empty;
		private string ordAccount = string.Empty;
		private string ordSide = string.Empty;
		private int ordTrusteeId;
		private long ordVolume;
		private string ordPrice = string.Empty;
		private long ordPublish;
		private string ordCondition = string.Empty;
		private string ordEntryId = string.Empty;
		private string publicIP = string.Empty;
		private string status = string.Empty;
		public int RefNo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.refNo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.refNo = value;
			}
		}
		public int Operator
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.operatorType;
			}
		}
		public string OperatorName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.operatorType == 1)
				{
					return ">=";
				}
				if (this.operatorType == 2)
				{
					return "<=";
				}
				if (this.operatorType == 3)
				{
					return ">";
				}
				if (this.operatorType == 4)
				{
					return "<";
				}
				return this.operatorType.ToString();
			}
		}
		public int FieldType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.fieldType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.fieldType = value;
			}
		}
		public string FieldName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.fieldType == 1)
				{
					return "Last";
				}
				if (this.fieldType == 2)
				{
					return "Bid";
				}
				if (this.fieldType == 3)
				{
					return "Offer";
				}
				if (this.fieldType == 4)
				{
					return "SMA";
				}
				if (this.fieldType == 5)
				{
					return "Break High";
				}
				if (this.fieldType == 6)
				{
					return "Break Low";
				}
				return this.FieldType.ToString();
			}
		}
		public int CheckPeriods
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.checkPeriods;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.checkPeriods = value;
			}
		}
		public decimal TmpPeriodPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tmpPeriodPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.tmpPeriodPrice = value;
			}
		}
		public decimal CheckPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.checkPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.checkPrice = value;
			}
		}
		public int StockNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.stockNumber;
			}
		}
		public string UserId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.userId;
			}
		}
		public string KmSession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.kmSession;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.kmSession = value;
			}
		}
		public string KmLocal
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.kmLocal;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.kmLocal = value;
			}
		}
		public string OrdAccount
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ordAccount;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.ordAccount = value;
			}
		}
		public string OrdSide
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ordSide;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.ordSide = value;
			}
		}
		public string OrdSideName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.OrdSide == "B")
				{
					return "Buy";
				}
				if (this.OrdSide == "S")
				{
					return "Sell";
				}
				if (this.OrdSide == "H")
				{
					return "Short";
				}
				if (this.OrdSide == "C")
				{
					return "Cover";
				}
				return "";
			}
		}
		public int OrdTrusteeId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ordTrusteeId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.ordTrusteeId = value;
			}
		}
		public long OrdVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ordVolume;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.ordVolume = value;
			}
		}
		public string OrdPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ordPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.ordPrice = value;
			}
		}
		public long OrdPublish
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ordPublish;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.ordPublish = value;
			}
		}
		public string OrdCondition
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ordCondition;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.ordCondition = value;
			}
		}
		public string OrdEntryId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ordEntryId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.ordEntryId = value;
			}
		}
		public string PublicIP
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.publicIP;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.publicIP = value;
			}
		}
		public string Status
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.status;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.status = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StopOrderCommand()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StopOrderCommand(int refNo, string userId, int stockNumber, int fieldType, int operatorType, decimal price, string ordAccount, string ordSide, int ordTtf, long ordVolume, string ordPrice, long ordPublish, string ordCondition, string ordEntryId, string pubIp, string kimengSession, string kimengLocal)
		{
			this.refNo = refNo;
			this.userId = userId;
			this.stockNumber = stockNumber;
			this.fieldType = fieldType;
			this.operatorType = operatorType;
			this.checkPrice = price;
			this.ordAccount = ordAccount;
			this.ordSide = ordSide;
			this.ordTrusteeId = ordTtf;
			this.ordVolume = ordVolume;
			this.ordPrice = ordPrice;
			this.ordPublish = ordPublish;
			this.ordCondition = ordCondition;
			this.ordEntryId = ordEntryId;
			this.publicIP = pubIp;
			this.kmSession = kimengSession;
			this.kmLocal = kimengLocal;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int refNo, string userId, int stockNumber, int fieldType, int operatorType, decimal price, string ordAccount, string ordSide, int ordTtf, long ordVolume, string ordPrice, long ordPublish, string ordCondition, string ordEntryId, string pubIp, string kimengSession, string kimengLocal)
		{
			return string.Concat(new object[]
			{
				this.messageType,
				refNo,
				';',
				userId,
				';',
				stockNumber,
				';',
				fieldType,
				';',
				operatorType,
				';',
				price,
				';',
				ordAccount,
				';',
				ordSide,
				';',
				ordTtf,
				';',
				ordVolume,
				';',
				ordPrice,
				';',
				ordPublish,
				';',
				ordCondition,
				';',
				ordEntryId,
				';',
				pubIp,
				';',
				kimengSession,
				';',
				kimengLocal
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				this.messageType = message.Substring(0, 2);
				string[] array = message.Substring(2).Split(new char[]
				{
					';'
				});
				this.refNo = Convert.ToInt32(array[0]);
				this.userId = array[1];
				this.stockNumber = Convert.ToInt32(array[2]);
				this.fieldType = Convert.ToInt32(array[3]);
				this.operatorType = Convert.ToInt32(array[4]);
				this.checkPrice = Convert.ToDecimal(array[5]);
				this.ordAccount = array[6];
				this.ordSide = array[7];
				int.TryParse(array[8], out this.ordTrusteeId);
				long.TryParse(array[9], out this.ordVolume);
				this.ordPrice = array[10];
				this.ordPublish = Convert.ToInt64(array[11]);
				this.ordCondition = array[12];
				this.ordEntryId = array[13];
				this.publicIP = array[14];
				this.kmSession = array[15];
				this.kmLocal = array[16];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
