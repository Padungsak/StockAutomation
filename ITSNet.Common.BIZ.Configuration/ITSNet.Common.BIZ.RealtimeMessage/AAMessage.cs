using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class AAMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private string side;
		private decimal price;
		private long volume;
		private int firm;
		private string board = string.Empty;
		private DateTime messageTime;
		private string addCancelFlag;
		private string contact;
		private string[] Arr;
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
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.firm = value;
			}
		}
		public string Board
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.board;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.board = value;
			}
		}
		public DateTime MessageTime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageTime;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.messageTime = value;
			}
		}
		public string AddCancelFlag
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.addCancelFlag;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.addCancelFlag = value;
			}
		}
		public string Contact
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.contact;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.contact = value;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "AA";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AAMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AAMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, string side, decimal price, long volume, int firm, string board, int messageTime, string addCancelFlag, string contact)
		{
			return string.Concat(new object[]
			{
				"AA",
				securityNumber,
				';',
				side,
				';',
				price,
				';',
				volume,
				';',
				firm,
				';',
				board,
				';',
				DateTime.Now.ToString("HH:mm:ss"),
				';',
				addCancelFlag,
				';',
				contact
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				message = message.Substring(2);
				this.Arr = message.Split(new char[]
				{
					';'
				});
				this.securityNumber = Convert.ToInt32(this.Arr[0]);
				this.side = this.Arr[1];
				this.price = Convert.ToDecimal(this.Arr[2]);
				this.volume = Convert.ToInt64(this.Arr[3]);
				this.firm = Convert.ToInt32(this.Arr[4]);
				this.board = this.Arr[5];
				if (FormatUtil.Isdatetime(this.Arr[6]))
				{
					this.messageTime = Convert.ToDateTime(this.Arr[6]);
				}
				this.addCancelFlag = this.Arr[7];
				this.contact = this.Arr[8];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
