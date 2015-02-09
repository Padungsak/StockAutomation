using i2TradePlus.Properties;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
namespace i2TradePlus.WebProxy
{
	public class Https
	{
		public delegate void OnErrorHandler(Exception ex);
		public delegate void OnTransferHandler(string text);
		public delegate void OnEndTransferHandler(int direction);
		public delegate void OnStartTransferHandler(int direction);
		public delegate void OnSSLServerAuthenticationHandler(HttpsSSLServerAuthenticationEventArgs e);
		private int readBufferSize = 1024;
		private int timeOut = 15000;
		//private WebProxy proxy = null;
        [CompilerGenerated]
        //private static RemoteCertificateValidationCallback <>9__CachedAnonymousMethodDelegate1;
        public Https.OnErrorHandler _OnError;
		public event Https.OnErrorHandler OnError
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
		public Https.OnTransferHandler _OnTransfer;
		public event Https.OnTransferHandler OnTransfer
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnTransfer += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnTransfer -= value;
            }
		}
		public Https.OnEndTransferHandler _OnEndTransfer;
		public event Https.OnEndTransferHandler OnEndTransfer
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnEndTransfer += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnEndTransfer -= value;
            }
		}
		public Https.OnStartTransferHandler _OnStartTransfer;
		public event Https.OnStartTransferHandler OnStartTransfer
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnStartTransfer += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnStartTransfer -= value;
            }
		}
		public Https.OnSSLServerAuthenticationHandler _OnSSLServerAuthentication;
		public event Https.OnSSLServerAuthenticationHandler OnSSLServerAuthentication
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnSSLServerAuthentication += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnSSLServerAuthentication -= value;
            }
		}
		public int ReadBufferSize
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.readBufferSize;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.readBufferSize = value;
			}
		}
		public int TimeOut
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Get(string URL)
		{
			CookieContainer cookies = new CookieContainer();
			HttpWebRequest webRequest = this.GetWebRequest(new Uri(URL), cookies);
			ServicePointManager.ServerCertificateValidationCallback = ((object obj, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors) => true);
			string result;
			try
			{
				if (this._OnStartTransfer != null)
				{
					this._OnStartTransfer(1);
				}
				HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream);
				char[] array = new char[this.ReadBufferSize];
				int i = streamReader.Read(array, 0, this.ReadBufferSize);
				string text = string.Empty;
				while (i > 0)
				{
					string text2 = new string(array, 0, i);
					text += text2;
					i = streamReader.Read(array, 0, this.ReadBufferSize);
					if (this._OnTransfer != null)
					{
						this._OnTransfer(text2);
					}
				}
				streamReader.Close();
				responseStream.Close();
				httpWebResponse.Close();
				if (this._OnEndTransfer != null)
				{
					this._OnEndTransfer(1);
				}
				result = text;
			}
			catch (Exception ex)
			{
				if (this._OnError != null)
				{
					this._OnError(ex);
				}
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private HttpWebRequest GetWebRequest(Uri uri, CookieContainer cookies)
		{
			HttpWebRequest result;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
				httpWebRequest.CookieContainer = cookies;
				httpWebRequest.Timeout = this.TimeOut;
				httpWebRequest.ReadWriteTimeout = 10000;
				httpWebRequest.KeepAlive = true;
				httpWebRequest.ProtocolVersion = HttpVersion.Version11;
				if (ApplicationInfo.IsUseProxy)
				{
                    //if (this.proxy == null)
                    //{
                    //    this.proxy = new WebProxy(Settings.Default.ProxyHost, Settings.Default.ProxyPort);
                    //    this.proxy.Credentials = new NetworkCredential(Settings.Default.ProxyUsername, ApplicationInfo.ProxyPassword);
                    //}
                    //httpWebRequest.Proxy = this.proxy;
				}
				result = httpWebRequest;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			HttpsSSLServerAuthenticationEventArgs httpsSSLServerAuthenticationEventArgs = new HttpsSSLServerAuthenticationEventArgs();
			if (this._OnSSLServerAuthentication != null)
			{
				this._OnSSLServerAuthentication(httpsSSLServerAuthenticationEventArgs);
			}
			return httpsSSLServerAuthenticationEventArgs.Accept;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetCookies(string URL)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\";
			Uri uri = new Uri(URL);
			string dnsSafeHost = uri.DnsSafeHost;
			string searchPattern = string.Empty;
			string text = Environment.UserName.ToLower();
			IPAddress iPAddress;
			if (IPAddress.TryParse(dnsSafeHost, out iPAddress))
			{
				string[] array = dnsSafeHost.Split(new char[]
				{
					'.'
				});
				searchPattern = string.Concat(new object[]
				{
					text,
					'@',
					array[0],
					'.',
					array[1],
					'.',
					array[2],
					'*'
				});
			}
			else
			{
				string[] array2 = dnsSafeHost.Split(new char[]
				{
					'.'
				});
				searchPattern = string.Concat(new object[]
				{
					text,
					'@',
					'*',
					array2[1],
					'*'
				});
			}
			string[] files = Directory.GetFiles(path, searchPattern);
			string result;
			if (files != null && files.Length > 0)
			{
				string text2 = File.ReadAllText(files[0]);
				string[] array3 = text2.Split(new char[]
				{
					'\n'
				});
				if (array3 != null && array3.Length > 2)
				{
					result = array3[1];
				}
				else
				{
					result = string.Empty;
				}
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Https()
		{
		}
	}
}
