using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class POMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private decimal projectedPrice;
		private long projectedVolume;
		public int SecurityNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNumber;
			}
		}
		public decimal ProjectedPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.projectedPrice;
			}
		}
		public long ProjectedVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.projectedVolume;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "PO";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public POMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public POMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, decimal projectedPrice, long projectedVolume, decimal breakClosePrice)
		{
			return "PO" + AutoTManager.Mod96((long)securityNumber, 3) + AutoTManager.Mode96dot(projectedPrice, 4) + AutoTManager.Mod96(projectedVolume, 5);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				message = message.Substring(2);
				this.securityNumber = (int)AutoTManager.Demod96int(message.Substring(0, 3));
				this.projectedPrice = AutoTManager.Demod96dot(message.Substring(3, 4));
				this.projectedVolume = AutoTManager.Demod96int(message.Substring(7, 5));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
