using i2TradePlus.Information;
using i2TradePlus.MyDataSet;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using System.Runtime.CompilerServices;

namespace i2TradePlus
{
    internal class AutoTradeManager : IRealtimeMessage
	{
        private static AutoTradeManager instance = null;
        internal static AutoTradeManager Instance
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                if (AutoTradeManager.instance == null)
                {
                    AutoTradeManager.instance = new AutoTradeManager();
                }
                return AutoTradeManager.instance;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                AutoTradeManager.instance = value;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
        {
            if (message.MessageType == "IE")
            {
                IEMessage iEMessage = (IEMessage)message;
                IndexStat.IndexItem indexItem = ApplicationInfo.IndexStatInfo[iEMessage.Symbol];
                if (indexItem != null)
                {
                    //int num = this.intzaSector.FindIndex("symbol", indexItem.Symbol);
                    //if (num > -1)
                    //{
                    //    this._recvDataRealTime.Symbol = indexItem.Symbol;
                    //    this._recvDataRealTime.Number = indexItem.Number;
                    //    this._recvDataRealTime.AccVolume = iEMessage.AccVolume;
                    //    this._recvDataRealTime.AccValue = iEMessage.AccValue;
                    //    this._recvDataRealTime.Index = iEMessage.IndexValue;
                    //    IndexStat.IndexItem indexItem2 = ApplicationInfo.IndexStatInfo[".SET"];
                    //    if (indexItem2 != null)
                    //    {
                    //        if (indexItem2.AccValue > 0m)
                    //        {
                    //            this._recvDataRealTime.Mkt = iEMessage.AccValue / indexItem2.AccValue * 100m;
                    //        }
                    //    }
                    //    this._recvDataRealTime.IndexPrior = indexItem.Prior;
                    //    this.UpdateToGrid(num + 1, this._recvDataRealTime);
                    //    if (base.IsAllowRender)
                    //    {
                    //        this.intzaSector.EndUpdate(num);
                    //    }
                    //}
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
        {
            int q = 0;
        }
	}
}
