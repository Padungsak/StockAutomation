using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class OSMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private decimal price;
		private decimal breakClosePrice;
		private int isLate5Min;
		public int SecurityNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNumber;
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
		public decimal BreakClosePrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.breakClosePrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.breakClosePrice = value;
			}
		}
		public int IsLate5Min
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isLate5Min;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isLate5Min = value;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "OS";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OSMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OSMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, decimal price, decimal breakClosePrice, int isLate5Min)
		{
			return string.Concat(new object[]
			{
				"OS",
				securityNumber,
				';',
				price,
				';',
				breakClosePrice,
				';',
				isLate5Min
			}).ToString();
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
				int.TryParse(array[0], out this.securityNumber);
				decimal.TryParse(array[1], out this.price);
				decimal.TryParse(array[2], out this.breakClosePrice);
				int.TryParse(array[3], out this.isLate5Min);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
