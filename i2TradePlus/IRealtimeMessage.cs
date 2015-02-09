using i2TradePlus.Information;
using ITSNet.Common.BIZ;
using System;
namespace i2TradePlus
{
	internal interface IRealtimeMessage
	{
		void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo);
		void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo);
	}
}
