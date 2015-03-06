using i2TradePlus.Information;
using i2TradePlus.Properties;
using i2TradePlus.Templates;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using STIControl;
using STIControl.CustomGrid;
using STIControl.SortTableGrid;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class ucBids : UserControl
	{
		public delegate void OnNewStockEventHandler(object sender, string stock);
		public delegate void OnLinkEventHandler(object sender, string stock, Point point);
		private delegate void setTopPriceColumeCallBack(bool isEquity);
		private delegate void UpdateTopPriceCallBack(StockList.StockInformation sInfo, string side, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, long volume1, long volume2, long volume3, long volume4, long volume5);
		private delegate void UpdateLastPriceCallback(StockList.StockInformation stockInfo, decimal lastPrice, decimal high, decimal low);
		private delegate void UpdateLastPriceTFEXCallback(SeriesList.SeriesInformation seriesInfo, decimal lastPrice, decimal high, decimal low);
		private delegate void UpdateAllVolumeCallBack(long openVolume, long buyVolume, long sellVolume);
		private delegate void UpdateLastSaleTickerCallBack(StockList.StockInformation stockInfo, decimal price, string side, long volume, string lastUpdate, int index);
		private delegate void DrawTPBlinkColorCallBack(LSAccumulate msgLS);
		private int _currStockNo;
		private bool isLoadingData = false;
		private IContainer components = null;
		private IntzaCustomGrid intzaHeader1;
		private IntzaCustomGrid intzaVolumeByBoard1;
		private SortGrid intzaLS1;
		private SortGrid intzaTP1;
		private TextBox txtStock;
        public ucBids.OnNewStockEventHandler _OnNewStock;
		public event ucBids.OnNewStockEventHandler OnNewStock
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnNewStock += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnNewStock -= value;
            }
		}
		public  ucBids.OnLinkEventHandler _OnLink;
		public event ucBids.OnLinkEventHandler OnLink
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._OnLink += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnLink -= value;
            }
		}
		public int CurrStockNo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._currStockNo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._currStockNo = value;
			}
		}
		public bool IsLoadingData
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isLoadingData;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isLoadingData = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ucBids()
		{
			try
			{
				this.InitializeComponent();
				if (!base.DesignMode)
				{
					this.txtStock.AutoCompleteMode = AutoCompleteMode.Suggest;
					this.txtStock.AutoCompleteSource = AutoCompleteSource.CustomSource;
					if (ApplicationInfo.IsSupportTfex)
					{
						if (ApplicationInfo.MultiAutoComp != null)
						{
							this.txtStock.AutoCompleteCustomSource = ApplicationInfo.MultiAutoComp;
						}
					}
					else
					{
						if (ApplicationInfo.StockAutoComp != null)
						{
							this.txtStock.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
						}
					}
				}
				this.SetResize();
			}
			catch (Exception ex)
			{
				this.ShowError("Init()", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetResize()
		{
			try
			{
				if (!base.DesignMode)
				{
					if (this.intzaHeader1.Font != Settings.Default.Default_Font)
					{
						this.intzaHeader1.Font = Settings.Default.Default_Font;
						this.intzaLS1.Font = Settings.Default.Default_Font;
						this.intzaTP1.Font = Settings.Default.Default_Font;
						this.intzaVolumeByBoard1.Font = Settings.Default.Default_Font;
					}
				}
				int width = base.ClientSize.Width;
				this.intzaHeader1.SetBounds(0, 0, width, this.intzaHeader1.GetHeightByRows());
				this.intzaTP1.SetBounds(0, this.intzaHeader1.Top + this.intzaHeader1.Height + 1, width * 63 / 100, this.intzaTP1.GetHeightByRows() - 1);
				this.intzaLS1.SetBounds(this.intzaTP1.Left + this.intzaTP1.Width + 1, this.intzaTP1.Top, width - (this.intzaTP1.Left + this.intzaTP1.Width + 1), this.intzaTP1.Height);
				this.intzaVolumeByBoard1.SetBounds(0, this.intzaTP1.Top + this.intzaTP1.Height + 1, width, this.intzaVolumeByBoard1.GetHeightByRows());
				base.Height = this.intzaVolumeByBoard1.Bottom;
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetBlinkModeTopPrice()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.SetBlinkModeTopPrice));
			}
			else
			{
				try
				{
					this.IsLoadingData = true;
					FormatType columnFormat = FormatType.BidOfferVolWhite;
					if (ApplicationInfo.IsSupportTPBlinkColor)
					{
						columnFormat = FormatType.BidOfferVolume;
					}
					for (int i = 0; i < this.intzaTP1.Rows; i++)
					{
						this.intzaTP1.Records(i).Fields("bidvolume").ColumnFormat = columnFormat;
						this.intzaTP1.Records(i).Fields("offervolume").ColumnFormat = columnFormat;
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SetBlinkModeTopPrice", ex);
				}
				this.IsLoadingData = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void setTopPriceColume(bool isEquity)
		{
			if (this.intzaTP1.InvokeRequired)
			{
				this.intzaTP1.Invoke(new ucBids.setTopPriceColumeCallBack(this.setTopPriceColume), new object[]
				{
					isEquity
				});
			}
			else
			{
				try
				{
					if (isEquity)
					{
						this.intzaTP1.GetColumn("bidvolume").Width = 31;
						this.intzaTP1.GetColumn("bid").Width = 19;
						this.intzaTP1.GetColumn("offer").Width = 19;
						this.intzaTP1.GetColumn("offervolume").Width = 31;
						this.intzaLS1.GetColumn("volume").Width = 48;
						this.intzaLS1.GetColumn("price").Width = 32;
					}
					else
					{
						this.intzaTP1.GetColumn("bidvolume").Width = 23;
						this.intzaTP1.GetColumn("bid").Width = 27;
						this.intzaTP1.GetColumn("offer").Width = 27;
						this.intzaTP1.GetColumn("offervolume").Width = 23;
						this.intzaLS1.GetColumn("volume").Width = 35;
						this.intzaLS1.GetColumn("price").Width = 45;
					}
				}
				catch (Exception ex)
				{
					this.ShowError("setTopPriceColume", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UpdateToControl(DataRow drStat, DataRow[] drTicker)
		{
			try
			{
				this.IsLoadingData = true;
				this._currStockNo = Convert.ToInt32(drStat["security_number"]);
				this.intzaHeader1.BeginUpdate();
				this.intzaLS1.BeginUpdate();
				this.intzaVolumeByBoard1.BeginUpdate();
				this.intzaTP1.BeginUpdate();
				this.setTopPriceColume(true);
				this.intzaHeader1.ClearAllText();
				this.intzaLS1.ClearAllText();
				this.intzaVolumeByBoard1.ClearAllText();
				this.intzaTP1.ClearAllText();
				StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this._currStockNo];
				this.intzaHeader1.Item("stock").Text = stockInformation.Symbol;
				this.UpdateTopPrice(stockInformation, "B", Convert.ToDecimal(drStat["bid_price1"].ToString()), Convert.ToDecimal(drStat["bid_price2"].ToString()), Convert.ToDecimal(drStat["bid_price3"].ToString()), Convert.ToDecimal(drStat["bid_price4"].ToString()), Convert.ToDecimal(drStat["bid_price5"].ToString()), Convert.ToInt64(drStat["bid_volume1"]), Convert.ToInt64(drStat["bid_volume2"]), Convert.ToInt64(drStat["bid_volume3"]), Convert.ToInt64(drStat["bid_volume4"]), Convert.ToInt64(drStat["bid_volume5"]));
				this.UpdateTopPrice(stockInformation, "S", Convert.ToDecimal(drStat["offer_price1"].ToString()), Convert.ToDecimal(drStat["offer_price2"].ToString()), Convert.ToDecimal(drStat["offer_price3"].ToString()), Convert.ToDecimal(drStat["offer_price4"].ToString()), Convert.ToDecimal(drStat["offer_price5"].ToString()), Convert.ToInt64(drStat["offer_volume1"]), Convert.ToInt64(drStat["offer_volume2"]), Convert.ToInt64(drStat["offer_volume3"]), Convert.ToInt64(drStat["offer_volume4"]), Convert.ToInt64(drStat["offer_volume5"]));
				this.UpdateLastPrice(stockInformation, Convert.ToDecimal(drStat["last_sale_price"]), Convert.ToDecimal(drStat["high_price"]), Convert.ToDecimal(drStat["low_price"]));
				this.UpdateAllVolume(Convert.ToInt64(drStat["open_volume"]), Convert.ToInt64(drStat["buy_volume"]), Convert.ToInt64(drStat["sell_volume"]));
				int num = 0;
				for (int i = 0; i < drTicker.Length; i++)
				{
					DataRow dataRow = drTicker[i];
					this.UpdateLastSaleTicker(stockInformation, Convert.ToDecimal(dataRow["price"]), dataRow["side"].ToString(), Convert.ToInt64(dataRow["volume"]), dataRow["last_update"].ToString(), num);
					num++;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl", ex);
			}
			finally
			{
				this.intzaHeader1.Redraw();
				this.intzaLS1.Redraw();
				this.intzaVolumeByBoard1.Redraw();
				this.intzaTP1.Redraw();
			}
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UpdateToControl_TFEX(DataRow drStat, DataRow[] drTicker)
		{
			try
			{
				string seriesName = drStat["sSeries"].ToString().Trim();
				this.intzaHeader1.BeginUpdate();
				this.intzaLS1.BeginUpdate();
				this.intzaVolumeByBoard1.BeginUpdate();
				this.intzaTP1.BeginUpdate();
				this.setTopPriceColume(false);
				this.intzaHeader1.ClearAllText();
				this.intzaLS1.ClearAllText();
				this.intzaVolumeByBoard1.ClearAllText();
				this.intzaTP1.ClearAllText();
				SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[seriesName];
				this.intzaHeader1.Item("stock").Text = seriesInformation.Symbol;
				int num;
				int.TryParse(drStat["iBidQuantity1"].ToString(), out num);
				int num2;
				int.TryParse(drStat["iBidQuantity2"].ToString(), out num2);
				int num3;
				int.TryParse(drStat["iBidQuantity3"].ToString(), out num3);
				int num4;
				int.TryParse(drStat["iBidQuantity4"].ToString(), out num4);
				int num5;
				int.TryParse(drStat["iBidQuantity5"].ToString(), out num5);
				int num6;
				int.TryParse(drStat["iAskQuantity1"].ToString(), out num6);
				int num7;
				int.TryParse(drStat["iAskQuantity2"].ToString(), out num7);
				int num8;
				int.TryParse(drStat["iAskQuantity3"].ToString(), out num8);
				int num9;
				int.TryParse(drStat["iAskQuantity4"].ToString(), out num9);
				int num10;
				int.TryParse(drStat["iAskQuantity5"].ToString(), out num10);
				this.UpdateTopPrice_TFEX(seriesInformation, "B", drStat["nmrBidPrice1"].ToString(), drStat["nmrBidPrice2"].ToString(), drStat["nmrBidPrice3"].ToString(), drStat["nmrBidPrice4"].ToString(), drStat["nmrBidPrice5"].ToString(), (long)num, (long)num2, (long)num3, (long)num4, (long)num5);
				this.UpdateTopPrice_TFEX(seriesInformation, "A", drStat["nmrAskPrice1"].ToString(), drStat["nmrAskPrice2"].ToString(), drStat["nmrAskPrice3"].ToString(), drStat["nmrAskPrice4"].ToString(), drStat["nmrAskPrice5"].ToString(), (long)num6, (long)num7, (long)num8, (long)num9, (long)num10);
				decimal num11;
				decimal.TryParse(drStat["nmrPrice"].ToString(), out num11);
				decimal high;
				decimal.TryParse(drStat["nmrHigh"].ToString(), out high);
				decimal low;
				decimal.TryParse(drStat["nmrLow"].ToString(), out low);
				this.UpdateLastPrice_TFEX(seriesInformation, num11, high, low);
				long num12;
				long.TryParse(drStat["OpenQty"].ToString(), out num12);
				long num13;
				long.TryParse(drStat["LongQty"].ToString(), out num13);
				long num14;
				long.TryParse(drStat["iTurnOver"].ToString(), out num14);
				long openInterest;
				long.TryParse(drStat["iOpenBalance"].ToString(), out openInterest);
				this.UpdateAllVolume_TFEX(num12, num13, num14 - num12 - num13, openInterest);
				long volume = 0L;
				for (int i = 0; i < drTicker.Length; i++)
				{
					long.TryParse(drTicker[i]["iQuantity"].ToString(), out volume);
					decimal.TryParse(drTicker[i]["nmrPrice"].ToString(), out num11);
					this.UpdateLastSaleTicker_TFEX(seriesInformation, num11, drTicker[i]["sSide"].ToString(), volume, Utilities.GetTimeLastSale(drTicker[i]["dtLastUpd"].ToString()), i);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl_TFEX", ex);
			}
			finally
			{
				this.intzaHeader1.Redraw();
				this.intzaLS1.Redraw();
				this.intzaVolumeByBoard1.Redraw();
				this.intzaTP1.Redraw();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTopPrice(StockList.StockInformation sInfo, string side, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, long volume1, long volume2, long volume3, long volume4, long volume5)
		{
			if (this.intzaTP1.InvokeRequired)
			{
				this.intzaTP1.Invoke(new ucBids.UpdateTopPriceCallBack(this.UpdateTopPrice), new object[]
				{
					sInfo,
					side,
					price1,
					price2,
					price3,
					price4,
					price5,
					volume1,
					volume2,
					volume3,
					volume4,
					volume5
				});
			}
			else
			{
				try
				{
					Color fontColor = (Brushes.Yellow as SolidBrush).Color;
					if (side == "B")
					{
						if (price1 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price1, sInfo);
							this.intzaTP1.Records(0).Fields("bidvolume").SetBidOfferVolumeText(volume1.ToString(), price1);
							this.intzaTP1.Records(0).Fields("bidvolume").FontColor = fontColor;
							this.intzaTP1.Records(0).Fields("bid").Text = Utilities.BidOfferPriceFormat(price1, volume1);
							this.intzaTP1.Records(0).Fields("bid").FontColor = fontColor;
						}
						if (price2 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price2, sInfo);
							this.intzaTP1.Records(1).Fields("bidvolume").SetBidOfferVolumeText(volume2.ToString(), price2);
							this.intzaTP1.Records(1).Fields("bidvolume").FontColor = fontColor;
							this.intzaTP1.Records(1).Fields("bid").Text = Utilities.PriceFormat(price2);
							this.intzaTP1.Records(1).Fields("bid").FontColor = fontColor;
						}
						if (price3 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price3, sInfo);
							this.intzaTP1.Records(2).Fields("bidvolume").SetBidOfferVolumeText(volume3.ToString(), price3);
							this.intzaTP1.Records(2).Fields("bidvolume").FontColor = fontColor;
							this.intzaTP1.Records(2).Fields("bid").Text = Utilities.PriceFormat(price3);
							this.intzaTP1.Records(2).Fields("bid").FontColor = fontColor;
						}
						if (price4 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price4, sInfo);
							this.intzaTP1.Records(3).Fields("bidvolume").SetBidOfferVolumeText(volume4.ToString(), price4);
							this.intzaTP1.Records(3).Fields("bidvolume").FontColor = fontColor;
							this.intzaTP1.Records(3).Fields("bid").Text = Utilities.PriceFormat(price4);
							this.intzaTP1.Records(3).Fields("bid").FontColor = fontColor;
						}
						if (price5 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price5, sInfo);
							this.intzaTP1.Records(4).Fields("bidvolume").SetBidOfferVolumeText(volume5.ToString(), price5);
							this.intzaTP1.Records(4).Fields("bidvolume").FontColor = fontColor;
							this.intzaTP1.Records(4).Fields("bid").Text = Utilities.PriceFormat(price5);
							this.intzaTP1.Records(4).Fields("bid").FontColor = fontColor;
						}
					}
					else
					{
						if (side == "S")
						{
							if (price1 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price1, sInfo);
								this.intzaTP1.Records(0).Fields("offervolume").SetBidOfferVolumeText(volume1.ToString(), price1);
								this.intzaTP1.Records(0).Fields("offervolume").FontColor = fontColor;
								this.intzaTP1.Records(0).Fields("offer").Text = Utilities.BidOfferPriceFormat(price1, volume1);
								this.intzaTP1.Records(0).Fields("offer").FontColor = fontColor;
							}
							if (price2 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price2, sInfo);
								this.intzaTP1.Records(1).Fields("offervolume").SetBidOfferVolumeText(volume2.ToString(), price2);
								this.intzaTP1.Records(1).Fields("offervolume").FontColor = fontColor;
								this.intzaTP1.Records(1).Fields("offer").Text = Utilities.PriceFormat(price2);
								this.intzaTP1.Records(1).Fields("offer").FontColor = fontColor;
							}
							if (price3 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price3, sInfo);
								this.intzaTP1.Records(2).Fields("offervolume").SetBidOfferVolumeText(volume3.ToString(), price3);
								this.intzaTP1.Records(2).Fields("offervolume").FontColor = fontColor;
								this.intzaTP1.Records(2).Fields("offer").Text = Utilities.PriceFormat(price3);
								this.intzaTP1.Records(2).Fields("offer").FontColor = fontColor;
							}
							if (price4 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price4, sInfo);
								this.intzaTP1.Records(3).Fields("offervolume").SetBidOfferVolumeText(volume4.ToString(), price4);
								this.intzaTP1.Records(3).Fields("offervolume").FontColor = fontColor;
								this.intzaTP1.Records(3).Fields("offer").Text = Utilities.PriceFormat(price4);
								this.intzaTP1.Records(3).Fields("offer").FontColor = fontColor;
							}
							if (price5 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price5, sInfo);
								this.intzaTP1.Records(4).Fields("offervolume").SetBidOfferVolumeText(volume5.ToString(), price5);
								this.intzaTP1.Records(4).Fields("offervolume").FontColor = fontColor;
								this.intzaTP1.Records(4).Fields("offer").Text = Utilities.PriceFormat(price5);
								this.intzaTP1.Records(4).Fields("offer").FontColor = fontColor;
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateTopPrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTopPrice_TFEX(SeriesList.SeriesInformation sInfo, string side, string price1, string price2, string price3, string price4, string price5, long volume1, long volume2, long volume3, long volume4, long volume5)
		{
			try
			{
				Color fontColor = (Brushes.Yellow as SolidBrush).Color;
				if (side == "B")
				{
					if (volume1 != -1L)
					{
						fontColor = Utilities.ComparePriceCFColor(price1, sInfo);
						this.intzaTP1.Records(0).Fields("bidvolume").SetBidOfferVolumeText(volume1.ToString(), price1);
						this.intzaTP1.Records(0).Fields("bidvolume").FontColor = fontColor;
						if (Utilities.GetTopPriceZero(volume1, price1))
						{
							this.intzaTP1.Records(0).Fields("bid").Text = "0.00";
						}
						else
						{
							this.intzaTP1.Records(0).Fields("bid").Text = Utilities.PriceFormat(price1, sInfo.NumOfDec);
						}
						this.intzaTP1.Records(0).Fields("bid").FontColor = fontColor;
					}
					if (volume2 != -1L)
					{
						fontColor = Utilities.ComparePriceCFColor(price2, sInfo);
						this.intzaTP1.Records(1).Fields("bidvolume").SetBidOfferVolumeText(volume2.ToString(), price2);
						this.intzaTP1.Records(1).Fields("bidvolume").FontColor = fontColor;
						if (Utilities.GetTopPriceZero(volume2, price2))
						{
							this.intzaTP1.Records(1).Fields("bid").Text = "0.00";
						}
						else
						{
							this.intzaTP1.Records(1).Fields("bid").Text = Utilities.PriceFormat(price2, sInfo.NumOfDec);
						}
						this.intzaTP1.Records(1).Fields("bid").FontColor = fontColor;
					}
					if (volume3 != -1L)
					{
						fontColor = Utilities.ComparePriceCFColor(price3, sInfo);
						this.intzaTP1.Records(2).Fields("bidvolume").SetBidOfferVolumeText(volume3.ToString(), price3);
						this.intzaTP1.Records(2).Fields("bidvolume").FontColor = fontColor;
						if (Utilities.GetTopPriceZero(volume3, price3))
						{
							this.intzaTP1.Records(2).Fields("bid").Text = "0.00";
						}
						else
						{
							this.intzaTP1.Records(2).Fields("bid").Text = Utilities.PriceFormat(price3, sInfo.NumOfDec);
						}
						this.intzaTP1.Records(2).Fields("bid").FontColor = fontColor;
					}
					if (volume4 != -1L)
					{
						fontColor = Utilities.ComparePriceCFColor(price4, sInfo);
						this.intzaTP1.Records(3).Fields("bidvolume").SetBidOfferVolumeText(volume4.ToString(), price4);
						this.intzaTP1.Records(3).Fields("bidvolume").FontColor = fontColor;
						if (Utilities.GetTopPriceZero(volume4, price4))
						{
							this.intzaTP1.Records(3).Fields("bid").Text = "0.00";
						}
						else
						{
							this.intzaTP1.Records(3).Fields("bid").Text = Utilities.PriceFormat(price4, sInfo.NumOfDec);
						}
						this.intzaTP1.Records(3).Fields("bid").FontColor = fontColor;
					}
					if (volume5 != -1L)
					{
						fontColor = Utilities.ComparePriceCFColor(price5, sInfo);
						this.intzaTP1.Records(4).Fields("bidvolume").SetBidOfferVolumeText(volume5.ToString(), price5);
						this.intzaTP1.Records(4).Fields("bidvolume").FontColor = fontColor;
						if (Utilities.GetTopPriceZero(volume5, price5))
						{
							this.intzaTP1.Records(4).Fields("bid").Text = "0.00";
						}
						else
						{
							this.intzaTP1.Records(4).Fields("bid").Text = Utilities.PriceFormat(price5, sInfo.NumOfDec);
						}
						this.intzaTP1.Records(4).Fields("bid").FontColor = fontColor;
					}
				}
				else
				{
					if (side == "A")
					{
						if (volume1 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price1, sInfo);
							this.intzaTP1.Records(0).Fields("offervolume").SetBidOfferVolumeText(volume1.ToString(), price1);
							this.intzaTP1.Records(0).Fields("offervolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume1, price1))
							{
								this.intzaTP1.Records(0).Fields("offer").Text = "0.00";
							}
							else
							{
								this.intzaTP1.Records(0).Fields("offer").Text = Utilities.PriceFormat(price1, sInfo.NumOfDec);
							}
							this.intzaTP1.Records(0).Fields("offer").FontColor = fontColor;
						}
						if (volume2 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price2, sInfo);
							this.intzaTP1.Records(1).Fields("offervolume").SetBidOfferVolumeText(volume2.ToString(), price2);
							this.intzaTP1.Records(1).Fields("offervolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume2, price2))
							{
								this.intzaTP1.Records(1).Fields("offer").Text = "0.00";
							}
							else
							{
								this.intzaTP1.Records(1).Fields("offer").Text = Utilities.PriceFormat(price2, sInfo.NumOfDec);
							}
							this.intzaTP1.Records(1).Fields("offer").FontColor = fontColor;
						}
						if (volume3 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price3, sInfo);
							this.intzaTP1.Records(2).Fields("offervolume").SetBidOfferVolumeText(volume3.ToString(), price3);
							this.intzaTP1.Records(2).Fields("offervolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume3, price3))
							{
								this.intzaTP1.Records(2).Fields("offer").Text = "0.00";
							}
							else
							{
								this.intzaTP1.Records(2).Fields("offer").Text = Utilities.PriceFormat(price3, sInfo.NumOfDec);
							}
							this.intzaTP1.Records(2).Fields("offer").FontColor = fontColor;
						}
						if (volume4 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price4, sInfo);
							this.intzaTP1.Records(3).Fields("offervolume").SetBidOfferVolumeText(volume4.ToString(), price4);
							this.intzaTP1.Records(3).Fields("offervolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume4, price4))
							{
								this.intzaTP1.Records(3).Fields("offer").Text = "0.00";
							}
							else
							{
								this.intzaTP1.Records(3).Fields("offer").Text = Utilities.PriceFormat(price4, sInfo.NumOfDec);
							}
							this.intzaTP1.Records(3).Fields("offer").FontColor = fontColor;
						}
						if (volume5 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price5, sInfo);
							this.intzaTP1.Records(4).Fields("offervolume").SetBidOfferVolumeText(volume5.ToString(), price5);
							this.intzaTP1.Records(4).Fields("offervolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume5, price5))
							{
								this.intzaTP1.Records(4).Fields("offer").Text = "0.00";
							}
							else
							{
								this.intzaTP1.Records(4).Fields("offer").Text = Utilities.PriceFormat(price5, sInfo.NumOfDec);
							}
							this.intzaTP1.Records(4).Fields("offer").FontColor = fontColor;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateTopPrice_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastPrice(StockList.StockInformation stockInfo, decimal lastPrice, decimal high, decimal low)
		{
			if (this.intzaHeader1.InvokeRequired)
			{
				this.intzaHeader1.Invoke(new ucBids.UpdateLastPriceCallback(this.UpdateLastPrice), new object[]
				{
					stockInfo,
					lastPrice,
					high,
					low
				});
			}
			else
			{
				try
				{
					if (stockInfo.PriorPrice > 0m && lastPrice > 0m)
					{
						Color fontColor = Utilities.ComparePriceCFColor(lastPrice, stockInfo);
						this.intzaHeader1.Item("price").Text = lastPrice.ToString();
						this.intzaHeader1.Item("price").FontColor = fontColor;
						this.intzaHeader1.Item("chg").Text = Utilities.PriceFormat(stockInfo.ChangePrice, true, "");
						this.intzaHeader1.Item("chg").FontColor = fontColor;
						this.intzaHeader1.Item("pchg").Text = stockInfo.ChangePricePct.ToString();
						this.intzaHeader1.Item("pchg").FontColor = fontColor;
					}
					else
					{
						lastPrice = stockInfo.PriorPrice;
						this.intzaHeader1.Item("price").Text = stockInfo.PriorPrice.ToString();
						this.intzaHeader1.Item("price").FontColor = Color.Yellow;
						this.intzaHeader1.Item("chg").Text = "";
						this.intzaHeader1.Item("pchg").Text = "";
					}
					this.intzaHeader1.Item("high").Text = high.ToString();
					this.intzaHeader1.Item("high").FontColor = Utilities.ComparePriceCFColor(high, stockInfo);
					this.intzaHeader1.Item("low").Text = low.ToString();
					this.intzaHeader1.Item("low").FontColor = Utilities.ComparePriceCFColor(low, stockInfo);
					this.intzaHeader1.Redraw();
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastSalePrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastPrice_TFEX(SeriesList.SeriesInformation seriesInfo, decimal lastPrice, decimal high, decimal low)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucBids.UpdateLastPriceTFEXCallback(this.UpdateLastPrice_TFEX), new object[]
				{
					seriesInfo,
					lastPrice,
					high,
					low
				});
			}
			else
			{
				try
				{
					if (seriesInfo.PrevFixPrice > 0m && lastPrice > 0m)
					{
						Color fontColor = Utilities.ComparePriceCFColor(lastPrice, seriesInfo);
						decimal num = lastPrice - seriesInfo.PrevFixPrice;
						decimal num2 = (lastPrice - seriesInfo.PrevFixPrice) / seriesInfo.PrevFixPrice * 100m;
						this.intzaHeader1.Item("price").Text = lastPrice.ToString();
						this.intzaHeader1.Item("price").FontColor = fontColor;
						this.intzaHeader1.Item("chg").Text = Utilities.PriceFormat(num, true, "");
						if (this.intzaHeader1.Item("chg").Text == string.Empty)
						{
							this.intzaHeader1.Item("chg").Text = "    ";
						}
						this.intzaHeader1.Item("chg").FontColor = fontColor;
						this.intzaHeader1.Item("pchg").Text = Utilities.PriceFormat(num2, true, "");
						if (this.intzaHeader1.Item("pchg").Text == string.Empty)
						{
							this.intzaHeader1.Item("pchg").Text = "        ";
						}
						this.intzaHeader1.Item("pchg").FontColor = fontColor;
					}
					else
					{
						lastPrice = seriesInfo.PrevFixPrice;
						this.intzaHeader1.Item("price").Text = seriesInfo.PrevFixPrice.ToString();
						this.intzaHeader1.Item("price").FontColor = Color.Yellow;
						this.intzaHeader1.Item("chg").Text = "";
						this.intzaHeader1.Item("pchg").Text = "";
					}
					this.intzaHeader1.Item("high").Text = Utilities.PriceFormat(high, seriesInfo.NumOfDec);
					this.intzaHeader1.Item("high").FontColor = Utilities.ComparePriceCFColor(high, seriesInfo);
					this.intzaHeader1.Item("low").Text = Utilities.PriceFormat(low, seriesInfo.NumOfDec);
					this.intzaHeader1.Item("low").FontColor = Utilities.ComparePriceCFColor(low, seriesInfo);
					this.intzaHeader1.EndUpdate();
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastSalePrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateAllVolume(long openVolume, long buyVolume, long sellVolume)
		{
			if (this.intzaVolumeByBoard1.InvokeRequired)
			{
				this.intzaVolumeByBoard1.Invoke(new ucBids.UpdateAllVolumeCallBack(this.UpdateAllVolume), new object[]
				{
					openVolume,
					buyVolume,
					sellVolume
				});
			}
			else
			{
				try
				{
					this.intzaVolumeByBoard1.Item("open_vol").Text = openVolume.ToString();
					this.intzaVolumeByBoard1.Item("buy_vol").Text = buyVolume.ToString();
					this.intzaVolumeByBoard1.Item("sell_vol").Text = sellVolume.ToString();
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateAllVolume", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateAllVolume_TFEX(long openVolume, long longVolume, long shortVolume, long openInterest)
		{
			try
			{
				this.intzaVolumeByBoard1.Item("open_vol").Text = openVolume.ToString();
				this.intzaVolumeByBoard1.Item("buy_vol").Text = longVolume.ToString();
				this.intzaVolumeByBoard1.Item("sell_vol").Text = shortVolume.ToString();
				this.intzaVolumeByBoard1.Item("OI_vol").Text = openInterest.ToString();
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateAllVolume_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastSaleTicker(StockList.StockInformation stockInfo, decimal price, string side, long volume, string lastUpdate, int index)
		{
			if (this.intzaLS1.InvokeRequired)
			{
				this.intzaLS1.Invoke(new ucBids.UpdateLastSaleTickerCallBack(this.UpdateLastSaleTicker), new object[]
				{
					stockInfo,
					price,
					side,
					volume,
					lastUpdate,
					index
				});
			}
			else
			{
				try
				{
					RecordItem recordItem;
					if (index == -1)
					{
						recordItem = this.intzaLS1.AddRecord(1, true);
					}
					else
					{
						recordItem = this.intzaLS1.Records(index);
					}
					recordItem.Fields("volume").Text = volume.ToString();
					recordItem.Fields("side").Text = side;
					recordItem.Fields("price").Text = Utilities.PriceFormat(price);
					Color fontColor = Utilities.ComparePriceCFColor(price, stockInfo);
					recordItem.Fields("price").FontColor = fontColor;
					fontColor = Color.Yellow;
					if (side == "B")
					{
						fontColor = Color.Cyan;
					}
					else
					{
						if (side == "S")
						{
							fontColor = Color.Magenta;
						}
					}
					recordItem.Fields("side").FontColor = fontColor;
					recordItem.Fields("volume").FontColor = fontColor;
					recordItem.Changed = true;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastSaleTicker", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastSaleTicker_TFEX(SeriesList.SeriesInformation stockInfo, decimal price, string side, long volume, string lastUpdate, int index)
		{
			try
			{
				RecordItem recordItem;
				if (index == -1)
				{
					recordItem = this.intzaLS1.AddRecord(1, true);
				}
				else
				{
					recordItem = this.intzaLS1.Records(index);
				}
				recordItem.Fields("side").Text = side.ToString();
				recordItem.Fields("volume").Text = volume.ToString();
				recordItem.Fields("price").Text = Utilities.PriceFormat(price, stockInfo.NumOfDec);
				Color fontColor = Utilities.ComparePriceCFColor(price, stockInfo);
				recordItem.Fields("price").FontColor = fontColor;
				if (side == "B")
				{
					recordItem.Fields("side").FontColor = Color.Cyan;
					recordItem.Fields("volume").FontColor = Color.Cyan;
				}
				else
				{
					if (side == "S")
					{
						recordItem.Fields("side").FontColor = Color.Magenta;
						recordItem.Fields("volume").FontColor = Color.Magenta;
					}
					else
					{
						recordItem.Fields("side").FontColor = Color.Yellow;
						recordItem.Fields("volume").FontColor = Color.Yellow;
					}
				}
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (!this.IsLoadingData)
			{
				try
				{
					string messageType = message.MessageType;
					if (messageType != null)
					{
						if (!(messageType == "TP"))
						{
							if (!(messageType == "L+"))
							{
								if (messageType == "SS")
								{
									SSMessage sSMessage = (SSMessage)message;
									if (ApplicationInfo.MarketState == "S")
									{
										this.UpdateLastPrice(realtimeStockInfo, realtimeStockInfo.PriorPrice, 0m, 0m);
									}
								}
							}
							else
							{
								LSAccumulate lSAccumulate = (LSAccumulate)message;
								if (lSAccumulate.SecurityNumber == this._currStockNo)
								{
									this.UpdateLastPrice(realtimeStockInfo, lSAccumulate.LastPrice, realtimeStockInfo.HighPrice, realtimeStockInfo.LowPrice);
									this.UpdateLastSaleTicker(realtimeStockInfo, lSAccumulate.LastPrice, lSAccumulate.Side, lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot, lSAccumulate.LastTime, -1);
									this.UpdateAllVolume(lSAccumulate.OpenVolume * (long)realtimeStockInfo.BoardLot, lSAccumulate.BuyVolume * (long)realtimeStockInfo.BoardLot, lSAccumulate.SellVolume * (long)realtimeStockInfo.BoardLot);
									this.intzaHeader1.EndUpdate();
									this.intzaLS1.Redraw();
									this.intzaVolumeByBoard1.EndUpdate();
									this.DrawTPBlinkColor(lSAccumulate);
								}
							}
						}
						else
						{
							TPMessage tPMessage = (TPMessage)message;
							if (tPMessage.SecurityNumber == this._currStockNo)
							{
								this.UpdateTopPrice(realtimeStockInfo, tPMessage.Side, tPMessage.Price1, tPMessage.Price2, tPMessage.Price3, tPMessage.Price4, tPMessage.Price5, tPMessage.Volume1 * (long)realtimeStockInfo.BoardLot, tPMessage.Volume2 * (long)realtimeStockInfo.BoardLot, tPMessage.Volume3 * (long)realtimeStockInfo.BoardLot, tPMessage.Volume4 * (long)realtimeStockInfo.BoardLot, tPMessage.Volume5 * (long)realtimeStockInfo.BoardLot);
								this.intzaTP1.EndUpdate();
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SecurityInfo::RecvMessage", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			if (!this.IsLoadingData)
			{
				try
				{
					if (message.MessageType == "TP")
					{
						if (realtimeSeriesInfo.Symbol != "")
						{
							TPMessageTFEX tPMessageTFEX = (TPMessageTFEX)message;
							this.UpdateTopPrice_TFEX(realtimeSeriesInfo, tPMessageTFEX.Side, tPMessageTFEX.Price1.ToString(), tPMessageTFEX.Price2.ToString(), tPMessageTFEX.Price3.ToString(), tPMessageTFEX.Price4.ToString(), tPMessageTFEX.Price5.ToString(), (long)tPMessageTFEX.Vol1, (long)tPMessageTFEX.Vol2, (long)tPMessageTFEX.Vol3, (long)tPMessageTFEX.Vol4, (long)tPMessageTFEX.Vol5);
							this.intzaTP1.EndUpdate();
						}
					}
					else
					{
						if (message.MessageType == "LS")
						{
							if (realtimeSeriesInfo.Symbol != "")
							{
								LSMessageTFEX lSMessageTFEX = (LSMessageTFEX)message;
								SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[lSMessageTFEX.Sec];
								this.UpdateLastPrice_TFEX(realtimeSeriesInfo, lSMessageTFEX.Price, lSMessageTFEX.High, lSMessageTFEX.Low);
								this.UpdateLastSaleTicker_TFEX(realtimeSeriesInfo, lSMessageTFEX.Price, lSMessageTFEX.Side, (long)lSMessageTFEX.Vol, lSMessageTFEX.LastTime, -1);
								this.UpdateAllVolume_TFEX((long)lSMessageTFEX.OpenQty, (long)lSMessageTFEX.LongQty, (long)lSMessageTFEX.ShortQty, (long)realtimeSeriesInfo.OpenInt);
								this.intzaHeader1.EndUpdate();
								this.intzaLS1.EndUpdate();
								this.intzaVolumeByBoard1.EndUpdate();
								if (ApplicationInfo.IsSupportTPBlinkColor)
								{
									int isBlink = 3;
									if (lSMessageTFEX.Side == "S")
									{
										string b = Utilities.PriceFormat(lSMessageTFEX.Price);
										for (int i = 0; i < 5; i++)
										{
											if (this.intzaTP1.Records(i).Fields("bidvolume").TempText == b)
											{
												this.intzaTP1.Records(i).Fields("bidvolume").IsBlink = isBlink;
												break;
											}
										}
									}
									else
									{
										if (lSMessageTFEX.Side == "B")
										{
											string b = Utilities.PriceFormat(lSMessageTFEX.Price);
											for (int i = 0; i < 5; i++)
											{
												if (this.intzaTP1.Records(i).Fields("offervolume").TempText == b)
												{
													this.intzaTP1.Records(i).Fields("offervolume").IsBlink = isBlink;
													break;
												}
											}
										}
									}
									this.intzaTP1.EndUpdate();
								}
								else
								{
									this.intzaTP1.EndUpdate();
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SecurityInfo::RecvMessage", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DrawTPBlinkColor(LSAccumulate msgLS)
		{
			if (this.intzaTP1.InvokeRequired)
			{
				this.intzaTP1.Invoke(new ucBids.DrawTPBlinkColorCallBack(this.DrawTPBlinkColor), new object[]
				{
					msgLS
				});
			}
			else
			{
				try
				{
					if (ApplicationInfo.IsSupportTPBlinkColor)
					{
						int isBlink = 3;
						if (msgLS.Side == "S")
						{
							string b = Utilities.PriceFormat(msgLS.LastPrice);
							for (int i = 0; i < 5; i++)
							{
								if (this.intzaTP1.Records(i).Fields("bidvolume").TempText == b)
								{
									this.intzaTP1.Records(i).Fields("bidvolume").IsBlink = isBlink;
									break;
								}
							}
						}
						else
						{
							if (msgLS.Side == "B")
							{
								string b = Utilities.PriceFormat(msgLS.LastPrice);
								for (int i = 0; i < 5; i++)
								{
									if (this.intzaTP1.Records(i).Fields("offervolume").TempText == b)
									{
										this.intzaTP1.Records(i).Fields("offervolume").IsBlink = isBlink;
										break;
									}
								}
							}
						}
						this.intzaTP1.EndUpdate();
					}
					else
					{
						this.intzaTP1.EndUpdate();
					}
				}
				catch (Exception ex)
				{
					this.ShowError("DrawTPBlinkColor", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtStock_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					if (keyCode != Keys.Escape)
					{
						switch (keyCode)
						{
						case Keys.Up:
						case Keys.Down:
							e.SuppressKeyPress = true;
							break;
						}
					}
					else
					{
						this.txtStock.Hide();
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(this.txtStock.Text))
					{
						StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this.txtStock.Text.ToUpper()];
						if (stockInformation.Number > 0)
						{
							if (this._OnNewStock != null)
							{
								this._OnNewStock(this, stockInformation.Symbol);
							}
						}
						else
						{
							SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[this.txtStock.Text.ToUpper()];
							if (seriesInformation.Symbol != string.Empty)
							{
								if (this._OnNewStock != null)
								{
									this._OnNewStock(this, seriesInformation.Symbol);
								}
							}
							else
							{
								this.txtStock.Text = this.intzaHeader1.Item("stock").Text;
								this.txtStock.SelectAll();
							}
						}
					}
					else
					{
						if (this._OnNewStock != null)
						{
							this._OnNewStock(this, "");
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("txtStock_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucBids_SizeChanged(object sender, EventArgs e)
		{
			this.SetResize();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ClearControl(bool needRedraw)
		{
			try
			{
				this._currStockNo = 0;
				this.intzaHeader1.ClearAllText();
				this.intzaLS1.ClearAllText();
				this.intzaVolumeByBoard1.ClearAllText();
				this.intzaTP1.ClearAllText();
				if (needRedraw)
				{
					this.intzaHeader1.Redraw();
					this.intzaLS1.Redraw();
					this.intzaVolumeByBoard1.Redraw();
					this.intzaTP1.Redraw();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ClearControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaTP_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0 && (e.Column.Name == "bid" || e.Column.Name == "offer"))
				{
					SortGrid sortGrid = (SortGrid)sender;
					string text = string.Empty;
					text = ApplicationInfo.StockInfo[this._currStockNo].Symbol;
					if (!string.IsNullOrEmpty(text))
					{
						if (Settings.Default.SmartOneClick)
						{
							string price = sortGrid.Records(e.RowIndex).Fields(e.Column.Name).Text.ToString();
							string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
							TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, text, price, Settings.Default.SmartClickVolume);
						}
						else
						{
							TemplateManager.Instance.MainForm.SendOrderBox.SetCurrentSymbol(text);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaTP_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaHeader_GridMouseClick(object sender, ItemGridMouseEventArgs e)
		{
			try
			{
				if (e.Item.Name == "stock")
				{
					if (e.Mouse.Button == MouseButtons.Left)
					{
						this.SetTextPosition();
					}
					else
					{
						if (e.Mouse.Button == MouseButtons.Right)
						{
							if (this._OnLink != null && !string.IsNullOrEmpty(e.Item.Text.ToString()))
							{
								this._OnLink(this, e.Item.Text.ToString(), new Point(e.Position.X, e.Position.Y));
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetTextPosition()
		{
			try
			{
				this.txtStock.Text = string.Empty;
				this.txtStock.Left = this.intzaHeader1.Left + this.intzaHeader1.Left + this.intzaHeader1.Item("stock").X * this.intzaHeader1.Width / 100;
				this.txtStock.Top = this.intzaHeader1.Top + this.intzaHeader1.Top;
				this.txtStock.Width = this.intzaHeader1.Item("stock").Width * this.intzaHeader1.Width / 100;
				this.txtStock.Text = this.intzaHeader1.Item("stock").Text;
				this.txtStock.Font = this.intzaHeader1.Font;
				this.txtStock.Visible = true;
				this.txtStock.BringToFront();
				this.txtStock.Focus();
				this.txtStock.SelectAll();
			}
			catch (Exception ex)
			{
				this.ShowError("SetTextPosition", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ResetToCurrentStock()
		{
			try
			{
				this.txtStock.Text = this.intzaHeader1.Item("stock").Text;
				this.txtStock.SelectAll();
			}
			catch (Exception ex)
			{
				this.ShowError("ResetToCurrentStock", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void HideTextBox()
		{
			this.txtStock.Hide();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			ColumnItem columnItem = new ColumnItem();
			ColumnItem columnItem2 = new ColumnItem();
			ColumnItem columnItem3 = new ColumnItem();
			ColumnItem columnItem4 = new ColumnItem();
			ColumnItem columnItem5 = new ColumnItem();
			ColumnItem columnItem6 = new ColumnItem();
			ColumnItem columnItem7 = new ColumnItem();
			ItemGrid itemGrid = new ItemGrid();
			ItemGrid itemGrid2 = new ItemGrid();
			ItemGrid itemGrid3 = new ItemGrid();
			ItemGrid itemGrid4 = new ItemGrid();
			ItemGrid itemGrid5 = new ItemGrid();
			ItemGrid itemGrid6 = new ItemGrid();
			ItemGrid itemGrid7 = new ItemGrid();
			ItemGrid itemGrid8 = new ItemGrid();
			ItemGrid itemGrid9 = new ItemGrid();
			ItemGrid itemGrid10 = new ItemGrid();
			ItemGrid itemGrid11 = new ItemGrid();
			ItemGrid itemGrid12 = new ItemGrid();
			ItemGrid itemGrid13 = new ItemGrid();
			ItemGrid itemGrid14 = new ItemGrid();
			ItemGrid itemGrid15 = new ItemGrid();
			ItemGrid itemGrid16 = new ItemGrid();
			this.txtStock = new TextBox();
			this.intzaTP1 = new SortGrid();
			this.intzaLS1 = new SortGrid();
			this.intzaHeader1 = new IntzaCustomGrid();
			this.intzaVolumeByBoard1 = new IntzaCustomGrid();
			base.SuspendLayout();
			this.txtStock.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.txtStock.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.txtStock.BackColor = Color.WhiteSmoke;
			this.txtStock.BorderStyle = BorderStyle.FixedSingle;
			this.txtStock.CharacterCasing = CharacterCasing.Upper;
			this.txtStock.ForeColor = Color.Black;
			this.txtStock.Location = new Point(67, 106);
			this.txtStock.Margin = new Padding(1);
			this.txtStock.MaxLength = 12;
			this.txtStock.Name = "txtStock";
			this.txtStock.Size = new Size(83, 20);
			this.txtStock.TabIndex = 70;
			this.txtStock.Visible = false;
			this.txtStock.KeyDown += new KeyEventHandler(this.txtStock_KeyDown);
			this.intzaTP1.AllowDrop = true;
			this.intzaTP1.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaTP1.CanBlink = true;
			this.intzaTP1.CanDrag = false;
			this.intzaTP1.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Far;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "bidvolume";
			columnItem.Text = "Volume";
			columnItem.ValueFormat = FormatType.BidOfferVolume;
			columnItem.Visible = true;
			columnItem.Width = 30;
			columnItem2.Alignment = StringAlignment.Far;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "bid";
			columnItem2.Text = "Bid";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 20;
			columnItem3.Alignment = StringAlignment.Far;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.LightGray;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "offer";
			columnItem3.Text = "Offer";
			columnItem3.ValueFormat = FormatType.Text;
			columnItem3.Visible = true;
			columnItem3.Width = 20;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "offervolume";
			columnItem4.Text = "Volume";
			columnItem4.ValueFormat = FormatType.BidOfferVolume;
			columnItem4.Visible = true;
			columnItem4.Width = 30;
			this.intzaTP1.Columns.Add(columnItem);
			this.intzaTP1.Columns.Add(columnItem2);
			this.intzaTP1.Columns.Add(columnItem3);
			this.intzaTP1.Columns.Add(columnItem4);
			this.intzaTP1.CurrentScroll = 0;
			this.intzaTP1.FocusItemIndex = -1;
			this.intzaTP1.ForeColor = Color.Black;
			this.intzaTP1.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaTP1.HeaderPctHeight = 80f;
			this.intzaTP1.IsAutoRepaint = true;
			this.intzaTP1.IsDrawFullRow = false;
			this.intzaTP1.IsDrawGrid = false;
			this.intzaTP1.IsDrawHeader = true;
			this.intzaTP1.IsScrollable = false;
			this.intzaTP1.Location = new Point(14, 36);
			this.intzaTP1.MainColumn = "";
			this.intzaTP1.Margin = new Padding(1);
			this.intzaTP1.Name = "intzaTP1";
			this.intzaTP1.Rows = 5;
			this.intzaTP1.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaTP1.RowSelectType = 0;
			this.intzaTP1.RowsVisible = 5;
			this.intzaTP1.ScrollChennelColor = Color.FromArgb(100, 100, 100);
			this.intzaTP1.Size = new Size(220, 45);
			this.intzaTP1.SortColumnName = "";
			this.intzaTP1.SortType = SortType.Desc;
			this.intzaTP1.TabIndex = 66;
			this.intzaTP1.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaTP_TableMouseClick);
			this.intzaLS1.AllowDrop = true;
			this.intzaLS1.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaLS1.CanBlink = false;
			this.intzaLS1.CanDrag = false;
			this.intzaLS1.CanGetMouseMove = false;
			columnItem5.Alignment = StringAlignment.Center;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.LightGray;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "side";
			columnItem5.Text = "B/S";
			columnItem5.ValueFormat = FormatType.Text;
			columnItem5.Visible = true;
			columnItem5.Width = 20;
			columnItem6.Alignment = StringAlignment.Far;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.ColumnAlignment = StringAlignment.Center;
			columnItem6.FontColor = Color.LightGray;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "volume";
			columnItem6.Text = "Volume";
			columnItem6.ValueFormat = FormatType.Volume;
			columnItem6.Visible = true;
			columnItem6.Width = 48;
			columnItem7.Alignment = StringAlignment.Far;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.ColumnAlignment = StringAlignment.Center;
			columnItem7.FontColor = Color.LightGray;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "price";
			columnItem7.Text = "Price";
			columnItem7.ValueFormat = FormatType.Text;
			columnItem7.Visible = true;
			columnItem7.Width = 32;
			this.intzaLS1.Columns.Add(columnItem5);
			this.intzaLS1.Columns.Add(columnItem6);
			this.intzaLS1.Columns.Add(columnItem7);
			this.intzaLS1.CurrentScroll = 0;
			this.intzaLS1.FocusItemIndex = -1;
			this.intzaLS1.ForeColor = Color.Black;
			this.intzaLS1.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaLS1.HeaderPctHeight = 80f;
			this.intzaLS1.IsAutoRepaint = true;
			this.intzaLS1.IsDrawFullRow = true;
			this.intzaLS1.IsDrawGrid = false;
			this.intzaLS1.IsDrawHeader = true;
			this.intzaLS1.IsScrollable = false;
			this.intzaLS1.Location = new Point(261, 37);
			this.intzaLS1.MainColumn = "";
			this.intzaLS1.Margin = new Padding(1);
			this.intzaLS1.Name = "intzaLS1";
			this.intzaLS1.Rows = 5;
			this.intzaLS1.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaLS1.RowSelectType = 0;
			this.intzaLS1.RowsVisible = 5;
			this.intzaLS1.ScrollChennelColor = Color.FromArgb(100, 100, 100);
			this.intzaLS1.Size = new Size(149, 38);
			this.intzaLS1.SortColumnName = "";
			this.intzaLS1.SortType = SortType.Desc;
			this.intzaLS1.TabIndex = 65;
			this.intzaHeader1.AllowDrop = true;
			this.intzaHeader1.BackColor = Color.Black;
			this.intzaHeader1.CanDrag = false;
			this.intzaHeader1.IsAutoRepaint = true;
			this.intzaHeader1.IsDroped = false;
			itemGrid.AdjustFontSize = 0f;
			itemGrid.Alignment = StringAlignment.Near;
			itemGrid.BackColor = Color.FromArgb(40, 40, 40);
			itemGrid.Changed = false;
			itemGrid.FieldType = ItemType.Text;
			itemGrid.FontColor = Color.Yellow;
			itemGrid.FontStyle = FontStyle.Regular;
			itemGrid.Height = 1f;
			itemGrid.IsBlink = 0;
			itemGrid.Name = "stock";
			itemGrid.Text = "";
			itemGrid.ValueFormat = FormatType.Text;
			itemGrid.Visible = true;
			itemGrid.Width = 18;
			itemGrid.X = 0;
			itemGrid.Y = 0f;
			itemGrid2.AdjustFontSize = 0f;
			itemGrid2.Alignment = StringAlignment.Near;
			itemGrid2.BackColor = Color.Black;
			itemGrid2.Changed = false;
			itemGrid2.FieldType = ItemType.Text;
			itemGrid2.FontColor = Color.Yellow;
			itemGrid2.FontStyle = FontStyle.Regular;
			itemGrid2.Height = 1f;
			itemGrid2.IsBlink = 0;
			itemGrid2.Name = "price";
			itemGrid2.Text = "";
			itemGrid2.ValueFormat = FormatType.Price;
			itemGrid2.Visible = true;
			itemGrid2.Width = 13;
			itemGrid2.X = 18;
			itemGrid2.Y = 0f;
			itemGrid3.AdjustFontSize = 0f;
			itemGrid3.Alignment = StringAlignment.Near;
			itemGrid3.BackColor = Color.Black;
			itemGrid3.Changed = false;
			itemGrid3.FieldType = ItemType.Text;
			itemGrid3.FontColor = Color.Yellow;
			itemGrid3.FontStyle = FontStyle.Regular;
			itemGrid3.Height = 1f;
			itemGrid3.IsBlink = 0;
			itemGrid3.Name = "chg";
			itemGrid3.Text = "";
			itemGrid3.ValueFormat = FormatType.ChangePrice;
			itemGrid3.Visible = true;
			itemGrid3.Width = 11;
			itemGrid3.X = 31;
			itemGrid3.Y = 0f;
			itemGrid4.AdjustFontSize = 0f;
			itemGrid4.Alignment = StringAlignment.Near;
			itemGrid4.BackColor = Color.Black;
			itemGrid4.Changed = false;
			itemGrid4.FieldType = ItemType.Text;
			itemGrid4.FontColor = Color.Yellow;
			itemGrid4.FontStyle = FontStyle.Regular;
			itemGrid4.Height = 1f;
			itemGrid4.IsBlink = 0;
			itemGrid4.Name = "pchg";
			itemGrid4.Text = "";
			itemGrid4.ValueFormat = FormatType.PercentChange;
			itemGrid4.Visible = true;
			itemGrid4.Width = 14;
			itemGrid4.X = 42;
			itemGrid4.Y = 0f;
			itemGrid5.AdjustFontSize = 0f;
			itemGrid5.Alignment = StringAlignment.Near;
			itemGrid5.BackColor = Color.Black;
			itemGrid5.Changed = false;
			itemGrid5.FieldType = ItemType.Label;
			itemGrid5.FontColor = Color.LightGray;
			itemGrid5.FontStyle = FontStyle.Regular;
			itemGrid5.Height = 1f;
			itemGrid5.IsBlink = 0;
			itemGrid5.Name = "col3";
			itemGrid5.Text = "High";
			itemGrid5.ValueFormat = FormatType.Text;
			itemGrid5.Visible = true;
			itemGrid5.Width = 10;
			itemGrid5.X = 56;
			itemGrid5.Y = 0f;
			itemGrid6.AdjustFontSize = 0f;
			itemGrid6.Alignment = StringAlignment.Near;
			itemGrid6.BackColor = Color.Black;
			itemGrid6.Changed = false;
			itemGrid6.FieldType = ItemType.Text;
			itemGrid6.FontColor = Color.Yellow;
			itemGrid6.FontStyle = FontStyle.Regular;
			itemGrid6.Height = 1f;
			itemGrid6.IsBlink = 0;
			itemGrid6.Name = "high";
			itemGrid6.Text = "";
			itemGrid6.ValueFormat = FormatType.Price;
			itemGrid6.Visible = true;
			itemGrid6.Width = 12;
			itemGrid6.X = 66;
			itemGrid6.Y = 0f;
			itemGrid7.AdjustFontSize = 0f;
			itemGrid7.Alignment = StringAlignment.Near;
			itemGrid7.BackColor = Color.Black;
			itemGrid7.Changed = false;
			itemGrid7.FieldType = ItemType.Label;
			itemGrid7.FontColor = Color.LightGray;
			itemGrid7.FontStyle = FontStyle.Regular;
			itemGrid7.Height = 1f;
			itemGrid7.IsBlink = 0;
			itemGrid7.Name = "col4";
			itemGrid7.Text = "Low";
			itemGrid7.ValueFormat = FormatType.Text;
			itemGrid7.Visible = true;
			itemGrid7.Width = 10;
			itemGrid7.X = 78;
			itemGrid7.Y = 0f;
			itemGrid8.AdjustFontSize = 0f;
			itemGrid8.Alignment = StringAlignment.Near;
			itemGrid8.BackColor = Color.Black;
			itemGrid8.Changed = false;
			itemGrid8.FieldType = ItemType.Text;
			itemGrid8.FontColor = Color.Yellow;
			itemGrid8.FontStyle = FontStyle.Regular;
			itemGrid8.Height = 1f;
			itemGrid8.IsBlink = 0;
			itemGrid8.Name = "low";
			itemGrid8.Text = "";
			itemGrid8.ValueFormat = FormatType.Price;
			itemGrid8.Visible = true;
			itemGrid8.Width = 12;
			itemGrid8.X = 88;
			itemGrid8.Y = 0f;
			this.intzaHeader1.Items.Add(itemGrid);
			this.intzaHeader1.Items.Add(itemGrid2);
			this.intzaHeader1.Items.Add(itemGrid3);
			this.intzaHeader1.Items.Add(itemGrid4);
			this.intzaHeader1.Items.Add(itemGrid5);
			this.intzaHeader1.Items.Add(itemGrid6);
			this.intzaHeader1.Items.Add(itemGrid7);
			this.intzaHeader1.Items.Add(itemGrid8);
			this.intzaHeader1.LineColor = Color.Red;
			this.intzaHeader1.Location = new Point(1, 1);
			this.intzaHeader1.Margin = new Padding(1);
			this.intzaHeader1.Name = "intzaHeader1";
			this.intzaHeader1.Padding = new Padding(1);
			this.intzaHeader1.Size = new Size(397, 17);
			this.intzaHeader1.TabIndex = 64;
			this.intzaHeader1.Tag = "1";
			this.intzaHeader1.GridMouseClick += new IntzaCustomGrid.GridMouseClickEventHandler(this.intzaHeader_GridMouseClick);
			this.intzaVolumeByBoard1.AllowDrop = true;
			this.intzaVolumeByBoard1.BackColor = Color.Black;
			this.intzaVolumeByBoard1.CanDrag = false;
			this.intzaVolumeByBoard1.IsAutoRepaint = true;
			this.intzaVolumeByBoard1.IsDroped = false;
			itemGrid9.AdjustFontSize = 0f;
			itemGrid9.Alignment = StringAlignment.Near;
			itemGrid9.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid9.Changed = false;
			itemGrid9.FieldType = ItemType.Label2;
			itemGrid9.FontColor = Color.LightGray;
			itemGrid9.FontStyle = FontStyle.Regular;
			itemGrid9.Height = 1f;
			itemGrid9.IsBlink = 0;
			itemGrid9.Name = "col1";
			itemGrid9.Text = "OpenV";
			itemGrid9.ValueFormat = FormatType.Text;
			itemGrid9.Visible = true;
			itemGrid9.Width = 10;
			itemGrid9.X = 0;
			itemGrid9.Y = 0f;
			itemGrid10.AdjustFontSize = 0f;
			itemGrid10.Alignment = StringAlignment.Near;
			itemGrid10.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid10.Changed = false;
			itemGrid10.FieldType = ItemType.Text;
			itemGrid10.FontColor = Color.Yellow;
			itemGrid10.FontStyle = FontStyle.Regular;
			itemGrid10.Height = 1f;
			itemGrid10.IsBlink = 0;
			itemGrid10.Name = "open_vol";
			itemGrid10.Text = "";
			itemGrid10.ValueFormat = FormatType.Volume;
			itemGrid10.Visible = true;
			itemGrid10.Width = 16;
			itemGrid10.X = 10;
			itemGrid10.Y = 0f;
			itemGrid11.AdjustFontSize = 0f;
			itemGrid11.Alignment = StringAlignment.Near;
			itemGrid11.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid11.Changed = false;
			itemGrid11.FieldType = ItemType.Label2;
			itemGrid11.FontColor = Color.LightGray;
			itemGrid11.FontStyle = FontStyle.Regular;
			itemGrid11.Height = 1f;
			itemGrid11.IsBlink = 0;
			itemGrid11.Name = "col2";
			itemGrid11.Text = "BuyV";
			itemGrid11.ValueFormat = FormatType.Text;
			itemGrid11.Visible = true;
			itemGrid11.Width = 10;
			itemGrid11.X = 26;
			itemGrid11.Y = 0f;
			itemGrid12.AdjustFontSize = 0f;
			itemGrid12.Alignment = StringAlignment.Near;
			itemGrid12.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid12.Changed = false;
			itemGrid12.FieldType = ItemType.Text;
			itemGrid12.FontColor = Color.Yellow;
			itemGrid12.FontStyle = FontStyle.Regular;
			itemGrid12.Height = 1f;
			itemGrid12.IsBlink = 0;
			itemGrid12.Name = "buy_vol";
			itemGrid12.Text = "";
			itemGrid12.ValueFormat = FormatType.Volume;
			itemGrid12.Visible = true;
			itemGrid12.Width = 19;
			itemGrid12.X = 36;
			itemGrid12.Y = 0f;
			itemGrid13.AdjustFontSize = 0f;
			itemGrid13.Alignment = StringAlignment.Near;
			itemGrid13.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid13.Changed = false;
			itemGrid13.FieldType = ItemType.Label2;
			itemGrid13.FontColor = Color.LightGray;
			itemGrid13.FontStyle = FontStyle.Regular;
			itemGrid13.Height = 1f;
			itemGrid13.IsBlink = 0;
			itemGrid13.Name = "col3";
			itemGrid13.Text = "SellV";
			itemGrid13.ValueFormat = FormatType.Text;
			itemGrid13.Visible = true;
			itemGrid13.Width = 9;
			itemGrid13.X = 55;
			itemGrid13.Y = 0f;
			itemGrid14.AdjustFontSize = 0f;
			itemGrid14.Alignment = StringAlignment.Near;
			itemGrid14.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid14.Changed = false;
			itemGrid14.FieldType = ItemType.Text;
			itemGrid14.FontColor = Color.Yellow;
			itemGrid14.FontStyle = FontStyle.Regular;
			itemGrid14.Height = 1f;
			itemGrid14.IsBlink = 0;
			itemGrid14.Name = "sell_vol";
			itemGrid14.Text = "";
			itemGrid14.ValueFormat = FormatType.Volume;
			itemGrid14.Visible = true;
			itemGrid14.Width = 19;
			itemGrid14.X = 64;
			itemGrid14.Y = 0f;
			itemGrid15.AdjustFontSize = 0f;
			itemGrid15.Alignment = StringAlignment.Near;
			itemGrid15.BackColor = Color.Black;
			itemGrid15.Changed = false;
			itemGrid15.FieldType = ItemType.Label2;
			itemGrid15.FontColor = Color.White;
			itemGrid15.FontStyle = FontStyle.Regular;
			itemGrid15.Height = 1f;
			itemGrid15.IsBlink = 0;
			itemGrid15.Name = "col4";
			itemGrid15.Text = "OI";
			itemGrid15.ValueFormat = FormatType.Text;
			itemGrid15.Visible = true;
			itemGrid15.Width = 7;
			itemGrid15.X = 83;
			itemGrid15.Y = 0f;
			itemGrid16.AdjustFontSize = 0f;
			itemGrid16.Alignment = StringAlignment.Near;
			itemGrid16.BackColor = Color.Black;
			itemGrid16.Changed = false;
			itemGrid16.FieldType = ItemType.Text;
			itemGrid16.FontColor = Color.Yellow;
			itemGrid16.FontStyle = FontStyle.Regular;
			itemGrid16.Height = 1f;
			itemGrid16.IsBlink = 0;
			itemGrid16.Name = "OI_vol";
			itemGrid16.Text = "";
			itemGrid16.ValueFormat = FormatType.Text;
			itemGrid16.Visible = true;
			itemGrid16.Width = 9;
			itemGrid16.X = 91;
			itemGrid16.Y = 0f;
			this.intzaVolumeByBoard1.Items.Add(itemGrid9);
			this.intzaVolumeByBoard1.Items.Add(itemGrid10);
			this.intzaVolumeByBoard1.Items.Add(itemGrid11);
			this.intzaVolumeByBoard1.Items.Add(itemGrid12);
			this.intzaVolumeByBoard1.Items.Add(itemGrid13);
			this.intzaVolumeByBoard1.Items.Add(itemGrid14);
			this.intzaVolumeByBoard1.Items.Add(itemGrid15);
			this.intzaVolumeByBoard1.Items.Add(itemGrid16);
			this.intzaVolumeByBoard1.LineColor = Color.Red;
			this.intzaVolumeByBoard1.Location = new Point(16, 86);
			this.intzaVolumeByBoard1.Margin = new Padding(1);
			this.intzaVolumeByBoard1.Name = "intzaVolumeByBoard1";
			this.intzaVolumeByBoard1.Padding = new Padding(1);
			this.intzaVolumeByBoard1.Size = new Size(457, 18);
			this.intzaVolumeByBoard1.TabIndex = 62;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Gray;
			base.Controls.Add(this.txtStock);
			base.Controls.Add(this.intzaTP1);
			base.Controls.Add(this.intzaLS1);
			base.Controls.Add(this.intzaHeader1);
			base.Controls.Add(this.intzaVolumeByBoard1);
			base.Name = "ucBids";
			base.Size = new Size(474, 161);
			base.SizeChanged += new EventHandler(this.ucBids_SizeChanged);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
