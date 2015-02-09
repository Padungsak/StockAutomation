using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class OLMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private string side;
		private decimal price;
		private long volume;
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
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.side = value;
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
		public long Volume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "OL";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OLMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OLMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, string side, decimal price, long volume)
		{
			return string.Concat(new object[]
			{
				"OL",
				securityNumber.ToString(),
				';',
				side,
				';',
				price.ToString(),
				';',
				volume.ToString()
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
				this.price = Convert.ToDecimal(array[2]);
				this.volume = Convert.ToInt64(array[3]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
