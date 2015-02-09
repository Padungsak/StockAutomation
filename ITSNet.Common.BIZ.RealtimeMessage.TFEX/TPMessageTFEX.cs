using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class TPMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private int orderBookId;
		private string side = string.Empty;
		private int vol1;
		private decimal price1;
		private int vol2;
		private decimal price2;
		private int vol3;
		private decimal price3;
		private int vol4;
		private decimal price4;
		private int vol5;
		private decimal price5;
		private static StringBuilder lsString = new StringBuilder();
		private string[] Arr;
		public int OrderBookId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderBookId;
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
		public int Vol1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.vol1;
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
		public int Vol2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.vol2;
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
		public int Vol3
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.vol3;
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
		public int Vol4
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.vol4;
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
		public int Vol5
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.vol5;
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
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "TP";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TPMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TPMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int orderBookId, string side, int vol1, decimal price1, int vol2, decimal price2, int vol3, decimal price3, int vol4, decimal price4, int vol5, decimal price5)
		{
			try
			{
				if (TPMessageTFEX.lsString.Length > 0)
				{
					TPMessageTFEX.lsString.Remove(0, TPMessageTFEX.lsString.Length);
				}
				TPMessageTFEX.lsString.Append("TP".PadRight(4, ' '));
				TPMessageTFEX.lsString.Append(orderBookId);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(side);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(vol1);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(price1);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(vol2);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(price2);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(vol3);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(price3);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(vol4);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(price4);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(vol5);
				TPMessageTFEX.lsString.Append(';');
				TPMessageTFEX.lsString.Append(price5);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return TPMessageTFEX.lsString.ToString();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				message = message.Substring(4);
				this.Arr = message.Split(new char[]
				{
					';'
				});
				int.TryParse(this.Arr[0], out this.orderBookId);
				this.side = this.Arr[1];
				int.TryParse(this.Arr[2], out this.vol1);
				decimal.TryParse(this.Arr[3], out this.price1);
				int.TryParse(this.Arr[4], out this.vol2);
				decimal.TryParse(this.Arr[5], out this.price2);
				int.TryParse(this.Arr[6], out this.vol3);
				decimal.TryParse(this.Arr[7], out this.price3);
				int.TryParse(this.Arr[8], out this.vol4);
				decimal.TryParse(this.Arr[9], out this.price4);
				int.TryParse(this.Arr[10], out this.vol5);
				decimal.TryParse(this.Arr[11], out this.price5);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
