using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.AlertMessage
{
	public class MailAlertCommand
	{
		private const char spliter = ';';
		private string messageType = "MC";
		private string groupId = string.Empty;
		private string message = string.Empty;
		private string shooter = string.Empty;
		public string GroupId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.groupId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.groupId = value;
			}
		}
		public string Message
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.message;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.message = value;
			}
		}
		public string Shooter
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.shooter;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.shooter = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string groupId, string message, string shooter)
		{
			return string.Concat(new object[]
			{
				"MC",
				groupId,
				';',
				message,
				';',
				shooter
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
				this.groupId = array[0];
				this.message = array[1];
				this.shooter = array[2];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MailAlertCommand()
		{
		}
	}
}
