using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class COMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private long volume;
		private decimal price;
		private string side = string.Empty;
		public int SecurityNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNumber;
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
		public string Side
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.side;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "CO";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public COMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public COMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, long volume, decimal price, string side)
		{
			return string.Concat(new object[]
			{
				"CO",
				securityNumber,
				';',
				volume,
				';',
				price,
				';',
				side
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
				this.securityNumber = Convert.ToInt32(array[0]);
				this.volume = Convert.ToInt64(array[1]);
				this.price = Convert.ToDecimal(array[2]);
				this.side = array[3];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
