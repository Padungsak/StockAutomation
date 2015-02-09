using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;
namespace i2TradePlus.WebProxy
{
	internal class FeedsWebProxy
	{
		internal delegate void OnDataInHandler(string message);
		internal delegate void OnErrorHandler(TransferEventArgs e);
		internal delegate void GetHttpCallBack(int seq);
		private char US_CHAR_RECORD_SEPERATOR = Convert.ToChar(23);
		private char US_CHAR3 = Convert.ToChar(3);
		private System.Timers.Timer timerForRefresh;
		private Https https;
		private Queue<string> _dataInQueue = null;
		private int _lastSeqRecv = -1;
		private string _recvMessage = string.Empty;
		private bool sslEnabled = false;
		private int timeOut = 5;
		private bool canConnectServer = false;
		private string urlForSyncHandler = string.Empty;
		private bool isServiceStarted = false;
		private bool isAreadySendOnStarted = false;
		private bool isRecv = false;
		internal FeedsWebProxy.OnDataInHandler _OnDataIN;
		internal event FeedsWebProxy.OnDataInHandler OnDataIN
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnDataIN += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnDataIN -= value;
            }
		}
		internal FeedsWebProxy.OnErrorHandler _OnError;
		internal event FeedsWebProxy.OnErrorHandler OnError
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnError += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnError -= value;
            }
		}
		internal EventHandler _OnGettingHttp;
		internal event EventHandler OnGettingHttp
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnGettingHttp += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnGettingHttp -= value;
            }
		}
		internal EventHandler _OnGettedHttp;
		internal event EventHandler OnGettedHttp
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnGettedHttp += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnGettedHttp -= value;
            }
		}
		internal EventHandler _OnStarted;
		internal event EventHandler OnStarted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnStarted += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnStarted -= value;
            }
		}
        internal EventHandler _OnStoped;
		internal event EventHandler OnStoped
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._OnStoped += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnStoped -= value;
            }
		}
		internal bool SSLEnabled
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sslEnabled;
			}
		}
		internal int TimeOut
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.timeOut;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.timeOut = value;
			}
		}
		internal bool CanConnectServer
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.canConnectServer;
			}
		}
		internal string UrlForSyncHandler
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.urlForSyncHandler;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.urlForSyncHandler = value;
			}
		}
		internal bool IsServiceStarted
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isServiceStarted;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal FeedsWebProxy()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void Start()
		{
			try
			{
				if (this.https != null)
				{
					this.https = null;
				}
				this.https = new Https();
				this.https.OnError -= new Https.OnErrorHandler(this.https_OnError);
				this.https.OnError += new Https.OnErrorHandler(this.https_OnError);
				this.https.OnTransfer -= new Https.OnTransferHandler(this.https_OnTransfer);
				this.https.OnTransfer += new Https.OnTransferHandler(this.https_OnTransfer);
				this.https.OnSSLServerAuthentication -= new Https.OnSSLServerAuthenticationHandler(this.https_OnSSLServerAuthentication);
				this.https.OnSSLServerAuthentication += new Https.OnSSLServerAuthenticationHandler(this.https_OnSSLServerAuthentication);
				this.https.OnEndTransfer -= new Https.OnEndTransferHandler(this.https_OnEndTransfer);
				this.https.OnEndTransfer += new Https.OnEndTransferHandler(this.https_OnEndTransfer);
				this.https.OnStartTransfer -= new Https.OnStartTransferHandler(this.https_OnStartTransfer);
				this.https.OnStartTransfer += new Https.OnStartTransferHandler(this.https_OnStartTransfer);
				this.canConnectServer = false;
				this.isRecv = false;
				if (this._dataInQueue != null)
				{
					object syncRoot;
					Monitor.Enter(syncRoot = ((ICollection)this._dataInQueue).SyncRoot);
					try
					{
						this._dataInQueue.Clear();
					}
					finally
					{
						Monitor.Exit(syncRoot);
					}
				}
				else
				{
					this._dataInQueue = new Queue<string>();
				}
				this.isAreadySendOnStarted = false;
				this.isServiceStarted = true;
				Thread thread = new Thread(new ThreadStart(this.SplitMessage));
				thread.Start();
				this._lastSeqRecv = -1;
				if (this.timerForRefresh == null)
				{
					this.timerForRefresh = new System.Timers.Timer();
					this.timerForRefresh.Elapsed += new ElapsedEventHandler(this.timerForRefresh_Elapsed);
				}
				this.timerForRefresh.Stop();
				this.timerForRefresh.Interval = (double)ApplicationInfo.PullInterval;
				this.timerForRefresh.Start();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void Stop()
		{
			try
			{
				this.timerForRefresh.Enabled = false;
				this.canConnectServer = false;
				this.isServiceStarted = false;
				this._lastSeqRecv = -1;
				if (this._dataInQueue != null)
				{
					object syncRoot;
					Monitor.Enter(syncRoot = ((ICollection)this._dataInQueue).SyncRoot);
					try
					{
						this._dataInQueue.Clear();
					}
					finally
					{
						Monitor.Exit(syncRoot);
					}
				}
				if (this._OnStoped != null)
				{
					this._OnStoped(this, EventArgs.Empty);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void https_OnError(Exception ex)
		{
			this.isRecv = false;
			this._lastSeqRecv = -1;
			this.isAreadySendOnStarted = false;
			this.canConnectServer = false;
			if (this.isServiceStarted)
			{
				if (this._OnError != null)
				{
					this._OnError(new TransferEventArgs(ex));
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void https_OnSSLServerAuthentication(HttpsSSLServerAuthenticationEventArgs e)
		{
			e.Accept = true;
			this.sslEnabled = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void https_OnStartTransfer(int direction)
		{
			this.isRecv = true;
			this._recvMessage = string.Empty;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void https_OnTransfer(string text)
		{
			this._recvMessage += text;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void https_OnEndTransfer(int direction)
		{
			try
			{
				if (this.isServiceStarted)
				{
					string[] array = this._recvMessage.Split(new char[]
					{
						this.US_CHAR3
					});
					if (array.Length == 3)
					{
						object syncRoot;
						Monitor.Enter(syncRoot = ((ICollection)this._dataInQueue).SyncRoot);
						try
						{
							this._dataInQueue.Enqueue(array[2]);
						}
						finally
						{
							Monitor.Exit(syncRoot);
						}
						int.TryParse(array[1], out this._lastSeqRecv);
					}
					array = null;
				}
				this.isRecv = false;
			}
			catch (Exception exception)
			{
				this.isRecv = false;
				if (this._OnError != null)
				{
					this._OnError(new TransferEventArgs(exception));
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SplitMessage()
		{
			try
			{
				string text = string.Empty;
				while (this.isServiceStarted)
				{
					object syncRoot;
					Monitor.Enter(syncRoot = ((ICollection)this._dataInQueue).SyncRoot);
					int i;
					try
					{
						i = this._dataInQueue.Count;
					}
					finally
					{
						Monitor.Exit(syncRoot);
					}
					while (i > 0)
					{
						Monitor.Enter(syncRoot = ((ICollection)this._dataInQueue).SyncRoot);
						try
						{
							text = this._dataInQueue.Dequeue();
						}
						finally
						{
							Monitor.Exit(syncRoot);
						}
						i--;
						if (this._OnDataIN != null)
						{
							string[] array = text.Split(new char[]
							{
								this.US_CHAR_RECORD_SEPERATOR
							});
							if (array.Length > 0)
							{
								for (int j = 0; j < array.Length; j++)
								{
									this._OnDataIN(array[j]);
								}
							}
						}
					}
					Thread.Sleep(50);
				}
			}
			catch (Exception exception)
			{
				if (this._OnError != null)
				{
					this._OnError(new TransferEventArgs(exception));
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private object ICollection(Queue<string> dataInQueue)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerForRefresh_Elapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				this.timerForRefresh.Stop();
				if (this.isServiceStarted)
				{
					this.GetHttp(this._lastSeqRecv);
					this.timerForRefresh.Start();
				}
			}
			catch
			{
				if (this.isServiceStarted)
				{
					this.timerForRefresh.Start();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GetHttp(int seq)
		{
			try
			{
				if (!this.isRecv)
				{
					if (this._OnGettingHttp != null)
					{
						this._OnGettingHttp(this, EventArgs.Empty);
					}
					this.https.Get(string.Concat(new object[]
					{
						this.UrlForSyncHandler,
						"?seq=",
						seq,
						"&session=",
						ApplicationInfo.AccInfo.CurrentAccount
					}));
					this.canConnectServer = true;
					if (!this.isAreadySendOnStarted && this.isServiceStarted)
					{
						if (this._OnStarted != null)
						{
							this.isAreadySendOnStarted = true;
							this._OnStarted(this, EventArgs.Empty);
						}
					}
				}
			}
			catch (Exception exception)
			{
				this.isRecv = false;
				this._lastSeqRecv = -1;
				this.canConnectServer = false;
				this.isAreadySendOnStarted = false;
				if (this.isServiceStarted)
				{
					if (this._OnError != null)
					{
						this._OnError(new TransferEventArgs(exception));
					}
				}
			}
			finally
			{
				if (this._OnGettedHttp != null)
				{
					this._OnGettedHttp(this, EventArgs.Empty);
				}
			}
		}
	}
}
