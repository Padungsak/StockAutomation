using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class NHMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int newsNumber;
		private string symbol = string.Empty;
		private string source = string.Empty;
		private string newsStoryLink = string.Empty;
		private DateTime lastUpdate = DateTime.MinValue;
		private string headlineText = string.Empty;
		public int NewsNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newsNumber;
			}
		}
		public string Symbol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.symbol;
			}
		}
		public string Source
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.source;
			}
		}
		public string NewsStoryLink
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newsStoryLink;
			}
		}
		public DateTime Lastupdate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastUpdate;
			}
		}
		public string HeadlineText
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.headlineText;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "NH";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public NHMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public NHMessage(string message)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int newsNumber, string symbol, string source, string newsStoryLink, DateTime lastUpdate, string headlineText)
		{
			return string.Concat(new object[]
			{
				"NH",
				newsNumber.ToString(),
				';',
				symbol,
				';',
				source,
				';',
				newsStoryLink,
				';',
				lastUpdate.ToString("yyyy/MM/dd HH:mm:ss"),
				';',
				headlineText
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			message.Substring(0, 2);
			if (this.MessageType == "NH")
			{
				message = message.Substring(2);
				string[] array = message.Split(new char[]
				{
					';'
				});
				this.newsNumber = Convert.ToInt32(array[0]);
				this.symbol = array[1];
				this.source = array[2];
				this.newsStoryLink = array[3];
				if (FormatUtil.Isdatetime(array[4]))
				{
					this.lastUpdate = Convert.ToDateTime(array[4]);
				}
				for (int i = 5; i < array.Length; i++)
				{
					this.headlineText += array[i];
					if (i < array.Length - 1)
					{
						this.headlineText += ';';
					}
				}
			}
		}
	}
}
