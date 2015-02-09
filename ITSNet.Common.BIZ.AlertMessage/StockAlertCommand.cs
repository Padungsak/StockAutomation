using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.AlertMessage
{
	public class StockAlertCommand
	{
		private const char spliter = ';';
		private string messageType = "SC";
		private string userId = string.Empty;
		private string stock = string.Empty;
		private string field = string.Empty;
		private int operatorType;
		private decimal value;
		private int updateMode;
		private string memo = string.Empty;
		private bool isNewVersion;
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
		public string Field
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.field;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.field = value;
			}
		}
		public int OperatorType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.operatorType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.operatorType = value;
			}
		}
		public decimal Value
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.value;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.value = value;
			}
		}
		public int UpdateMode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.updateMode;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.updateMode = value;
			}
		}
		public string Memo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.memo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.memo = value;
			}
		}
		public bool IsNewVersion
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isNewVersion;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isNewVersion = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StockAlertCommand()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string userId, string stock, string field, int operatorType, decimal value, int updateMode, string memo, bool isNewVersion)
		{
			return string.Concat(new object[]
			{
				"SC",
				userId,
				';',
				stock,
				';',
				field,
				';',
				operatorType,
				';',
				value,
				';',
				updateMode,
				';',
				memo,
				';',
				isNewVersion
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
				this.stock = array[1];
				this.field = array[2];
				int.TryParse(array[3], out this.operatorType);
				decimal.TryParse(array[4], out this.value);
				int.TryParse(array[5], out this.updateMode);
				this.memo = array[6];
				if (array.Length >= 8)
				{
					bool.TryParse(array[7], out this.isNewVersion);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
