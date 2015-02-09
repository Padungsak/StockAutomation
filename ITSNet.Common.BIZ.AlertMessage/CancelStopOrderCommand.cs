using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.AlertMessage
{
	public class CancelStopOrderCommand
	{
		private const char spliter = ';';
		private string messageType = "TC";
		private int refNo;
		private string userId = string.Empty;
		private int stockNo;
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
		public int StockNo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.stockNo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.stockNo = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CancelStopOrderCommand()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int refNo, string userId, int stockNo)
		{
			return string.Concat(new object[]
			{
				this.messageType,
				refNo,
				';',
				userId,
				';',
				stockNo
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
				this.stockNo = Convert.ToInt32(array[2]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
