using i2TradePlus.Information;
using i2TradePlus.MyDataSet;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
namespace i2TradePlus
{
	internal class AlertManager : IRealtimeMessage
	{
		internal delegate void OnAlertHandler(AlertItem e);
		private struct AlertBcItem
		{
			internal string MessageType;
			internal string Symbol;
			internal decimal LastPrice;
			internal long Volume;
			internal decimal ChangePrice;
			internal decimal ChangePricePct;
			internal string Side;
			internal decimal BidPrice;
			internal decimal OfferPrice;
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal AlertBcItem(string messageType, decimal lastPrice, long volume, string symbol, decimal changePrice, decimal changePricePct, long boardLot)
			{
				this.MessageType = messageType;
				this.LastPrice = lastPrice;
				this.Volume = volume * boardLot;
				this.Symbol = symbol;
				this.ChangePrice = changePrice;
				this.ChangePricePct = changePricePct;
				this.Side = string.Empty;
				this.BidPrice = 0m;
				this.OfferPrice = 0m;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal AlertBcItem(string messageType, string symbol, string side, decimal price1)
			{
				this.MessageType = messageType;
				this.BidPrice = 0m;
				this.OfferPrice = 0m;
				this.Side = side;
				if (side == "B")
				{
					this.BidPrice = price1;
				}
				else
				{
					if (side == "S")
					{
						this.OfferPrice = price1;
					}
				}
				this.Symbol = symbol;
				this.LastPrice = 0m;
				this.Volume = 0L;
				this.ChangePrice = 0m;
				this.ChangePricePct = 0m;
			}
		}
		private static AlertManager instance = null;
		private Dictionary<string, AlertItemCollection> symbols = null;
		private Queue<AlertManager.AlertBcItem> _bcMessages = new Queue<AlertManager.AlertBcItem>();
		private bool isMonitoring = false;
        internal AlertManager.OnAlertHandler _OnAlert;
		internal event AlertManager.OnAlertHandler OnAlert
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnAlert += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnAlert -= value;
			}
		}
		internal static AlertManager Instance
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (AlertManager.instance == null)
				{
					AlertManager.instance = new AlertManager();
				}
				return AlertManager.instance;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				AlertManager.instance = value;
			}
		}
		internal Dictionary<string, AlertItemCollection> Symbols
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.symbols == null)
				{
					this.symbols = new Dictionary<string, AlertItemCollection>();
				}
				return this.symbols;
			}
		}
		internal bool IsMonitoring
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isMonitoring;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isMonitoring = value;
				if (value)
				{
					Thread thread = new Thread(new ThreadStart(this.MessageMonitoring));
					thread.Start();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal AlertManager()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddItem(LSAccumulate msgLS, string symbol, decimal changePrice, decimal changePricePct, long boardLot)
		{
			if (this.IsMonitoring)
			{
				object syncRoot;
				Monitor.Enter(syncRoot = ((ICollection)this._bcMessages).SyncRoot);
				try
				{
					this._bcMessages.Enqueue(new AlertManager.AlertBcItem(msgLS.MessageType, msgLS.LastPrice, msgLS.Volume, symbol, changePrice, changePricePct, boardLot));
				}
				finally
				{
					Monitor.Exit(syncRoot);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddItem(TPMessage msgTP, string symbol)
		{
			if (this.IsMonitoring)
			{
				object syncRoot;
				Monitor.Enter(syncRoot = ((ICollection)this._bcMessages).SyncRoot);
				try
				{
					this._bcMessages.Enqueue(new AlertManager.AlertBcItem(msgTP.MessageType, symbol, msgTP.Side, msgTP.Price1));
				}
				finally
				{
					Monitor.Exit(syncRoot);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddItem(LSMessageTFEX msgLS, string symbol, decimal changePrice, decimal changePricePct, long boardLot)
		{
			if (this.IsMonitoring)
			{
				object syncRoot;
				Monitor.Enter(syncRoot = ((ICollection)this._bcMessages).SyncRoot);
				try
				{
					this._bcMessages.Enqueue(new AlertManager.AlertBcItem("L+", msgLS.Price, (long)msgLS.Vol, symbol, changePrice, changePricePct, boardLot));
				}
				finally
				{
					Monitor.Exit(syncRoot);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal string GetOrderFilePath()
		{
			return ApplicationInfo.WORKING_ROOTPATH + "\\AlertOption\\" + ApplicationInfo.UserLoginID + "\\AlertOrder.xml";
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ReadData()
		{
			try
			{
				if (this.symbols == null)
				{
					this.symbols = new Dictionary<string, AlertItemCollection>();
				}
				else
				{
					this.symbols.Clear();
				}
				AlertCustomerExpressionDS alertCustomerExpressionDS = new AlertCustomerExpressionDS();
				string orderFilePath = this.GetOrderFilePath();
				if (File.Exists(orderFilePath))
				{
					alertCustomerExpressionDS.ReadXml(orderFilePath);
					this.ImportFromXML(alertCustomerExpressionDS);
				}
				alertCustomerExpressionDS.Clear();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ImportFromXML(DataSet ds)
		{
			try
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					Dictionary<string, AlertItemCollection> obj;
					Monitor.Enter(obj = this.symbols);
					try
					{
						string key = string.Empty;
						try
						{
							bool flag = ds.Tables[0].Columns.Contains("AlertTime");
							bool flag2 = ds.Tables[0].Columns.Contains("AlertValue");
							for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
							{
								key = ds.Tables[0].Rows[i]["Symbol"].ToString();
								if (!this.symbols.ContainsKey(key))
								{
									this.symbols.Add(key, new AlertItemCollection());
								}
								AlertItem alertItem = new AlertItem();
								alertItem.Symbol = ds.Tables[0].Rows[i]["Symbol"].ToString();
								alertItem.Field = ds.Tables[0].Rows[i]["ColumnsAlert"].ToString();
								alertItem.Operator = ds.Tables[0].Rows[i]["Operator"].ToString();
								alertItem.Value = ds.Tables[0].Rows[i]["Values"].ToString();
								alertItem.IsReaded = false;
								if (flag && !string.IsNullOrEmpty(ds.Tables[0].Rows[i]["AlertTime"].ToString()))
								{
									alertItem.AlertTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["AlertTime"].ToString());
								}
								else
								{
									alertItem.AlertTime = DateTime.MinValue;
								}
								if (flag2 && !string.IsNullOrEmpty(ds.Tables[0].Rows[i]["AlertValue"].ToString()))
								{
									alertItem.ValueMessageRealtime = ds.Tables[0].Rows[i]["AlertValue"].ToString();
								}
								else
								{
									alertItem.ValueMessageRealtime = string.Empty;
								}
								this.symbols[alertItem.Symbol].Add(alertItem);
							}
						}
						catch (Exception ex)
						{
							throw ex;
						}
					}
					finally
					{
						Monitor.Exit(obj);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal string ExportToXML()
		{
			throw new NotImplementedException();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveXML()
		{
			try
			{
				string orderFilePath = AlertManager.Instance.GetOrderFilePath();
				AlertCustomerExpressionDS alertCustomerExpressionDS = new AlertCustomerExpressionDS();
				if (!Directory.Exists(ApplicationInfo.WORKING_ROOTPATH + "\\AlertOption\\" + ApplicationInfo.UserLoginID))
				{
					Directory.CreateDirectory(ApplicationInfo.WORKING_ROOTPATH + "\\AlertOption\\" + ApplicationInfo.UserLoginID);
				}
				foreach (KeyValuePair<string, AlertItemCollection> current in AlertManager.Instance.Symbols)
				{
					foreach (AlertItem current2 in current.Value)
					{
						DataRow dataRow = alertCustomerExpressionDS.AlertCollection.NewRow();
						dataRow["Symbol"] = current2.Symbol;
						dataRow["ColumnsAlert"] = current2.Field;
						dataRow["Operator"] = current2.Operator;
						dataRow["Values"] = current2.Value;
						dataRow["AlertTime"] = current2.AlertTime.ToString();
						dataRow["AlertValue"] = current2.ValueMessageRealtime;
						alertCustomerExpressionDS.AlertCollection.Rows.Add(dataRow);
					}
				}
				if (alertCustomerExpressionDS.AlertCollection != null)
				{
					alertCustomerExpressionDS.WriteXml(orderFilePath);
				}
				alertCustomerExpressionDS = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MessageMonitoring()
		{
			while (this.isMonitoring)
			{
				int i = 0;
				object syncRoot;
				Monitor.Enter(syncRoot = ((ICollection)this._bcMessages).SyncRoot);
				try
				{
					i = this._bcMessages.Count;
				}
				finally
				{
					Monitor.Exit(syncRoot);
				}
				while (i > 0)
				{
					try
					{
						Monitor.Enter(syncRoot = ((ICollection)this._bcMessages).SyncRoot);
						AlertManager.AlertBcItem alertBcItem;
						try
						{
							alertBcItem = this._bcMessages.Dequeue();
						}
						finally
						{
							Monitor.Exit(syncRoot);
						}
						i--;
						if (alertBcItem.Symbol != null)
						{
							AlertItemCollection alertItemCollection = null;
							if (this.symbols.TryGetValue(alertBcItem.Symbol, out alertItemCollection))
							{
								foreach (AlertItem current in alertItemCollection)
								{
									bool flag = false;
									if (current.AlertTime == DateTime.MinValue)
									{
										if (alertBcItem.MessageType == "L+")
										{
											string text = current.Field.ToLower();
											if (text != null)
											{
												if (!(text == "lastprice"))
												{
													if (text == "%change")
													{
														if (this.ConditionCompare(current, alertBcItem.ChangePricePct))
														{
															current.ValueMessageRealtime = Math.Round(alertBcItem.ChangePricePct, 4).ToString();
															flag = true;
														}
													}
												}
												else
												{
													if (this.ConditionCompare(current, alertBcItem.LastPrice))
													{
														current.ValueMessageRealtime = alertBcItem.LastPrice.ToString();
														flag = true;
													}
												}
											}
										}
									}
									if (flag)
									{
										bool flag2 = false;
										if (!current.IsReaded)
										{
											current.IsReaded = true;
											current.AlertTime = DateTime.Now;
											flag2 = true;
										}
										if (flag2 && this._OnAlert != null)
										{
											this._OnAlert(new AlertItem(current));
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						ExceptionManager.Show(new ErrorItem(DateTime.Now, "AlertManager", "MessageMonitoring", ex.Message, ex.ToString()));
					}
					if (!this.isMonitoring)
					{
						break;
					}
				}
				Thread.Sleep(30);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ConditionCompare(AlertItem alertItem, decimal itemValue)
		{
			bool result = false;
			decimal d;
			decimal.TryParse(alertItem.Value, out d);
			if (alertItem.Operator == "=")
			{
				result = (Math.Round(itemValue, 4) == Math.Round(d, 4));
			}
			else
			{
				if (alertItem.Operator == ">=")
				{
					result = (Math.Round(itemValue, 4) >= Math.Round(d, 4));
				}
				else
				{
					if (alertItem.Operator == "<=")
					{
						result = (Math.Round(itemValue, 4) <= Math.Round(d, 4));
					}
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ConditionCompare(AlertItem alertItem, int itemValue)
		{
			bool result = false;
			int num;
			int.TryParse(alertItem.Value, out num);
			if (alertItem.Operator == "=")
			{
				result = (itemValue == num);
			}
			else
			{
				if (alertItem.Operator == ">=")
				{
					result = (itemValue >= num);
				}
				else
				{
					if (alertItem.Operator == "<=")
					{
						result = (itemValue <= num);
					}
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool ConditionCompare(AlertItem alertItem, long itemValue)
		{
			bool result = false;
			long num;
			long.TryParse(alertItem.Value, out num);
			if (alertItem.Operator == "=")
			{
				result = (itemValue == num);
			}
			else
			{
				if (alertItem.Operator == ">=")
				{
					result = (itemValue >= num);
				}
				else
				{
					if (alertItem.Operator == "<=")
					{
						result = (itemValue <= num);
					}
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal bool IsMessageForAlertMonitor(IBroadcastMessage item)
		{
			string empty = string.Empty;
			return this.IsMessageForAlertMonitor(item, out empty);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal bool IsMessageForAlertMonitor(IBroadcastMessage item, out string symbol)
		{
			bool flag = false;
			string text = string.Empty;
			bool result;
			if (item == null)
			{
				symbol = text;
				result = flag;
			}
			else
			{
				string messageType = item.MessageType;
				if (messageType != null)
				{
					if (!(messageType == "L+"))
					{
						if (messageType == "PD")
						{
							text = ApplicationInfo.StockInfo[(item as PDMessage).SecurityNumber].Symbol;
							flag = true;
						}
					}
					else
					{
						text = ApplicationInfo.StockInfo[(item as LSAccumulate).SecurityNumber].Symbol;
						flag = true;
					}
				}
				symbol = text;
				result = flag;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void MarkUnReadAll()
		{
			if (this.symbols != null)
			{
				foreach (KeyValuePair<string, AlertItemCollection> current in this.symbols)
				{
					foreach (AlertItem current2 in current.Value)
					{
						current2.IsReaded = false;
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void MarkUnRead(string symbol, AlertItem item)
		{
			AlertItemCollection alertItemCollection = null;
			if (this.symbols.TryGetValue(symbol, out alertItemCollection))
			{
				if (alertItemCollection.Contains(item))
				{
					item.IsReaded = false;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			try
			{
				string messageType = message.MessageType;
				if (messageType != null)
				{
					if (messageType == "L+")
					{
						if (ApplicationInfo.AlertOpen)
						{
							this.AddItem((LSAccumulate)message, realtimeStockInfo.Symbol, realtimeStockInfo.ChangePrice, realtimeStockInfo.ChangePricePct, (long)realtimeStockInfo.BoardLot);
						}
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			try
			{
				string messageType = message.MessageType;
				if (messageType != null)
				{
					if (messageType == "LS")
					{
						if (ApplicationInfo.AlertOpen)
						{
							this.AddItem((LSMessageTFEX)message, realtimeSeriesInfo.Symbol, realtimeSeriesInfo.ChangePrice, realtimeSeriesInfo.ChangePricePct, 1L);
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
