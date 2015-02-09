using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class TPMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private string side = string.Empty;
		private decimal price1;
		private long volume1;
		private decimal price2;
		private long volume2;
		private decimal price3;
		private long volume3;
		private decimal price4;
		private long volume4;
		private decimal price5;
		private long volume5;
		public int SecurityNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNumber;
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
		public decimal Price1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price1;
			}
		}
		public long Volume1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume1;
			}
		}
		public decimal Price2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price2;
			}
		}
		public long Volume2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume2;
			}
		}
		public decimal Price3
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price3;
			}
		}
		public long Volume3
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume3;
			}
		}
		public decimal Price4
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price4;
			}
		}
		public long Volume4
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume4;
			}
		}
		public decimal Price5
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price5;
			}
		}
		public long Volume5
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume5;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "TP";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TPMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TPMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, string side, decimal price1, long volume1, decimal price2, long volume2, decimal price3, long volume3, decimal price4, long volume4, decimal price5, long volume5)
		{
			return string.Concat(new object[]
			{
				"TP",
				securityNumber,
				';',
				side,
				';',
				price1,
				';',
				volume1,
				';',
				price2,
				';',
				volume2,
				';',
				price3,
				';',
				volume3,
				';',
				price4,
				';',
				volume4,
				';',
				price5,
				';',
				volume5
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
				this.side = array[1];
				decimal.TryParse(array[2], out this.price1);
				this.volume1 = Convert.ToInt64(array[3]);
				decimal.TryParse(array[4], out this.price2);
				this.volume2 = Convert.ToInt64(array[5]);
				decimal.TryParse(array[6], out this.price3);
				this.volume3 = Convert.ToInt64(array[7]);
				decimal.TryParse(array[8], out this.price4);
				this.volume4 = Convert.ToInt64(array[9]);
				decimal.TryParse(array[10], out this.price5);
				this.volume5 = Convert.ToInt64(array[11]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
