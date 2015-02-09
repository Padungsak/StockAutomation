using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.AlertMessage
{
	public class PortAlertCommand
	{
		private const char spliter = ';';
		private string messageType = "PC";
		private string userId = string.Empty;
		private string account = string.Empty;
		private string stock = string.Empty;
		private string sType = string.Empty;
		private int trusteeId;
		private decimal costPct;
		private decimal pricePct;
		private decimal sellPct;
		private long refNo;
		public string UserId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.userId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.userId = value;
			}
		}
		public string Account
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.account;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.account = value;
			}
		}
		public string Stock
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.stock;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.stock = value;
			}
		}
		public string SType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.sType = value;
			}
		}
		public int TrusteeId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.trusteeId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.trusteeId = value;
			}
		}
		public decimal CostPct
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.costPct;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.costPct = value;
			}
		}
		public decimal PricePct
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.pricePct;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.pricePct = value;
			}
		}
		public decimal SellPct
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sellPct;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.sellPct = value;
			}
		}
		public long RefNo
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string userId, string account, string stock, string sType, int trusteeId, decimal costPct, decimal pricePct, decimal sellPct, long refNo)
		{
			return string.Concat(new object[]
			{
				this.messageType,
				userId,
				';',
				account,
				';',
				stock,
				';',
				sType,
				';',
				trusteeId,
				';',
				costPct,
				';',
				pricePct,
				';',
				sellPct,
				';',
				refNo
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
				this.userId = array[0];
				this.account = array[1];
				this.stock = array[2];
				this.sType = array[3];
				int.TryParse(array[4], out this.trusteeId);
				decimal.TryParse(array[5], out this.costPct);
				decimal.TryParse(array[6], out this.pricePct);
				decimal.TryParse(array[7], out this.sellPct);
				long.TryParse(array[8], out this.refNo);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PortAlertCommand()
		{
		}
	}
}
