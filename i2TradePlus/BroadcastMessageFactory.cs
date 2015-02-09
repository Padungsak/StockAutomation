using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using System;
using System.Runtime.CompilerServices;
namespace i2TradePlus
{
	internal class BroadcastMessageFactory
	{
		private LOMessage loMessage = null;
		private PDMessage pdMessage = null;
		private SCMessage scMessage = null;
		private AAMessage aaMessage = null;
		private NHMessage nhMessage = null;
		private BroadCastOrderMessageClient broadcastMessageClient = null;
		private DGWOrderReplyMessage dgwOrderReply = null;
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal BroadcastMessageFactory()
		{
			this.loMessage = new LOMessage();
			this.pdMessage = new PDMessage();
			this.scMessage = new SCMessage();
			this.aaMessage = new AAMessage();
			this.nhMessage = new NHMessage();
			this.broadcastMessageClient = new BroadCastOrderMessageClient();
			this.dgwOrderReply = new DGWOrderReplyMessage();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal IBroadcastMessage CreateSETMessage(string message)
		{
			IBroadcastMessage result;
			try
			{
				string text = message.Substring(0, 2);
				switch (text)
				{
				case "TP":
					result = new TPMessage(message);
					return result;
				case "L+":
					result = new LSAccumulate(message);
					return result;
				case "PO":
					result = new POMessage(message);
					return result;
				case "B+":
					result = new BCTextMessage(message);
					return result;
				case "IS":
					result = new ISMessage(message);
					return result;
				case "IE":
					result = new IEMessage(message);
					return result;
				case "LO":
					this.loMessage.Unpack(message);
					result = this.loMessage;
					return result;
				case "MT":
					result = new MarketInfo(message);
					return result;
				case "PD":
					this.pdMessage.Unpack(message);
					result = this.pdMessage;
					return result;
				case "SC":
					this.scMessage.Unpack(message);
					result = this.scMessage;
					return result;
				case "SS":
					result = new SSMessage(message);
					return result;
				case "AA":
					this.aaMessage.Unpack(message);
					result = this.aaMessage;
					return result;
				case "NH":
					this.nhMessage.Unpack(message);
					result = this.nhMessage;
					return result;
				case "BA":
					result = new BAMessage(message);
					return result;
				case "0I":
					result = new OrderInfoClient(message);
					return result;
				case "0B":
					this.broadcastMessageClient.Unpack(message);
					result = this.broadcastMessageClient;
					return result;
				case "0G":
					this.dgwOrderReply.Unpack(message);
					result = this.dgwOrderReply;
					return result;
				}
				result = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal IBroadcastMessage CreateTfexMessage(string message)
		{
			IBroadcastMessage result;
			try
			{
				string text = message.Substring(0, 4).Trim();
				switch (text)
				{
				case "TP":
					result = new TPMessageTFEX(message);
					return result;
				case "LS":
					result = new LSMessageTFEX(message);
					return result;
				case "IUE":
					result = new IUEMessageTFEX(message);
					return result;
				case "MI":
					result = new MIMessageTFEX(message);
					return result;
				case "BC":
					result = new BCMessageTFEX(message);
					return result;
				case "CA8":
					result = new CA8MessageTFEX(message);
					return result;
				case "IU":
					result = new IUMessageTFEX(message);
					return result;
				case "PO":
					result = new POMessageTFEX(message);
					return result;
				case "SD":
					result = new SDMessageTFEX(message);
					return result;
				case "SS":
					result = new SSMessageTFEX(message);
					return result;
				case "ST":
					result = new STMessageTFEX(message);
					return result;
				case "SU":
					result = new SUMessageTFEX(message);
					return result;
				case "BU10":
					result = new BU10MessageTFEX(message);
					return result;
				case "#T9I":
					result = new OrderTFEXInfoClient(message);
					return result;
				case "TCF":
					result = new TCFMessageTFEX(message);
					return result;
				}
				result = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
	}
}
