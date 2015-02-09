using i2TradePlus.Properties;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class frmLogIn : Form
	{
		private delegate void CheckSettingProxyCallBack(bool isCheck);
		private delegate void ReEnableInputPasswordCallback(bool isEnable);
		private enum LoginResult
		{
			Success,
			Fail,
			InvalidVersion
		}
		private delegate void ShowSplashMessageCallback(string message);
		private BackgroundWorker bgwLoading = null;
		private BackgroundWorker bgwCheckProxy = null;
		private string _userId = string.Empty;
		private string _userPassword = string.Empty;
		private frmSplash splashForm = null;
		private bool _isRequireProxy = false;
		private bool _isRetryProxey = false;
		private frmLogIn.LoginResult _loginResult = frmLogIn.LoginResult.Fail;
		private string _loginResultMessage = string.Empty;
		private frmMain frm = null;
		private IContainer components = null;
		private Label lbCopyRight;
		private TextBox txtPassword;
		private TextBox txtUserID;
		private Button btnCancel;
		private Button btnLogin;
		private Label lbUserID;
		private Label lbPassword;
		private Panel pnLogin;
		private Label label1;
		private CheckBox chkRememberPwd;
		private TextBox txtProxyPort;
		private TextBox txtProxyHost;
		private TextBox txtProxyPassword;
		private TextBox txtProxyUserName;
		private Label label4;
		private Label label5;
		private Label label3;
		private Label label2;
		private GroupBox gbProxy;
		private CheckBox chkSettingProxy;
		private Label lblNote;
		private PictureBox pictureBox1;
		private CheckBox chbSupportTfex;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmLogIn()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmLogIn_Load(object sender, EventArgs e)
		{
			try
			{
				this.Text = "i2Trade Plus V." + Application.ProductVersion;
				this.OpenProxySetting();
				this.bgwLoading = new BackgroundWorker();
				this.bgwLoading.WorkerReportsProgress = true;
				this.bgwLoading.DoWork += new DoWorkEventHandler(this.bgwLoading_DoWork);
				this.bgwLoading.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwLoading_RunWorkerCompleted);
				int brokerId = ApplicationInfo.BrokerId;
				if (brokerId != 4 && brokerId != 8)
				{
					switch (brokerId)
					{
					case 11:
					case 13:
						goto IL_9C;
					}
					this.chbSupportTfex.Visible = false;
					goto IL_D6;
				}
				IL_9C:
				this.chbSupportTfex.Checked = Settings.Default.RequestTfex;
				this.chbSupportTfex.Visible = true;
				IL_D6:;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtUserID_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Up:
					e.SuppressKeyPress = true;
					break;
				case Keys.Down:
					this.txtPassword.Focus();
					break;
				}
			}
			else
			{
				this.txtPassword.Focus();
				this.txtPassword.SelectAll();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				if (keyCode == Keys.Up)
				{
					this.txtUserID.Focus();
					this.txtUserID.SelectAll();
				}
			}
			else
			{
				e.SuppressKeyPress = true;
				if (this.CheckValidateRequireField())
				{
					this.LoginProcessing();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnOk_Click(object sender, EventArgs e)
		{
			if (this.CheckValidateRequireField())
			{
				this.LoginProcessing();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbLoginMode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.txtUserID.Focus();
				this.txtUserID.SelectAll();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool CheckValidateRequireField()
		{
			bool result = false;
			if (string.IsNullOrEmpty(this.txtUserID.Text))
			{
				MessageBox.Show("Invalid User Id!", "LogIn!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				this.txtUserID.Focus();
			}
			else
			{
				if (string.IsNullOrEmpty(this.txtPassword.Text) && !ApplicationInfo.IsOpenFromWeb)
				{
					MessageBox.Show("Invalid Password!", "LogIn!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.txtPassword.Focus();
				}
				else
				{
					result = true;
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoginProcessing()
		{
			try
			{
				this.txtPassword.Enabled = false;
				this.btnLogin.Enabled = false;
				this.splashForm = new frmSplash();
				this.splashForm.StartPosition = FormStartPosition.Manual;
				ApplicationInfo.IsSupportTfex = this.chbSupportTfex.Checked;
				this._userId = this.txtUserID.Text.Trim();
				Settings.Default.LastLoginId = this._userId;
				Settings.Default.RequestTfex = this.chbSupportTfex.Checked;
				if (ApplicationInfo.IsOpenFromWeb)
				{
					this._userPassword = ApplicationInfo.AuthenKey;
				}
				else
				{
					this._userPassword = this.txtPassword.Text.Trim();
				}
				if (this.chkSettingProxy.Checked)
				{
					this.SaveProxySetting();
				}
				if (this.bgwCheckProxy == null)
				{
					this.bgwCheckProxy = new BackgroundWorker();
					this.bgwCheckProxy.WorkerReportsProgress = true;
					this.bgwCheckProxy.DoWork += new DoWorkEventHandler(this.bgwCheckProxy_DoWork);
					this.bgwCheckProxy.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwCheckProxy_RunWorkerCompleted);
				}
				if (!this.bgwCheckProxy.IsBusy)
				{
					this.bgwCheckProxy.RunWorkerAsync();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("LoginProcessing", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwCheckProxy_DoWork(object sender, DoWorkEventArgs e)
		{
			while (true)
			{
				try
				{
					this.Seti2InfoURL(ApplicationInfo.BrokerId);
					this._isRequireProxy = false;
					break;
				}
				catch (Exception ex)
				{
					this.CloseSplashMessage();
					if (ex.ToString().IndexOf("407") <= -1)
					{
						MessageBox.Show(ex.Message);
						this.ReEnableInputPassword(true);
						this._isRequireProxy = true;
						break;
					}
					this._isRequireProxy = true;
					if (this._isRetryProxey || !(Settings.Default.ProxyHost != string.Empty) || Settings.Default.ProxyPort <= 0 || !(Settings.Default.ProxyUsername != string.Empty) || !(Settings.Default.ProxyPassword != string.Empty))
					{
						if (this._isRetryProxey)
						{
							MessageBox.Show("Invalid proxy config, please check your account or password. ");
						}
						this.CheckSettingProxy(true);
						this.OpenProxySetting();
						this.ReEnableInputPassword(true);
						break;
					}
					this._isRetryProxey = true;
					ApplicationInfo.IsUseProxy = true;
					ApplicationInfo.ProxyPassword = Settings.Default.ProxyPassword;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwCheckProxy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (!this._isRequireProxy)
				{
					Settings.Default.LastLoginId = this.txtUserID.Text.Trim();
					Settings.Default.Save();
					this.bgwLoading.RunWorkerAsync();
				}
				else
				{
					this.txtPassword.Enabled = true;
					this.btnLogin.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Seti2InfoURL(int BrokerId)
		{
			string host = string.Empty;
			try
			{
				if (BrokerId == 4)
				{
					host = "https://tradings.maybank-ke.co.th/i2trade/i2info_MBKET_PLUS_3_0_16_0.ashx";
				}
				else
				{
					if (BrokerId == 1)
					{
						host = "http://www.cns.co.th/i2trade/i2info_CNS_PLUS_3_0_16_0.ashx";
					}
					else
					{
						if (BrokerId == 9)
						{
							host = "http://www.ktbst.co.th/i2trade/i2info_KTBST_PLUS_2_16_0_0.ashx";
						}
						else
						{
							if (BrokerId == 3)
							{
								host = "https://sso.osk188.co.th/i2trade/i2info_RHBOSK_PLUS_3_0_16_0.ashx";
							}
							else
							{
								if (BrokerId == 2)
								{
									host = "http://58.97.9.130/i2trade/i2info.ashx";
								}
								else
								{
									if (BrokerId == 8)
									{
										if (ApplicationInfo.Isi2infoLink2)
										{
											host = "https://asp.asiaplus.co.th/aspapp/i2trade/i2info_ASP_PLUS_3_0_16_0_second_i2info.php";
										}
										else
										{
											host = "https://asp.asiaplus.co.th/aspapp/i2trade/i2info_ASP_PLUS_3_0_16_0.php";
										}
									}
									else
									{
										if (BrokerId == 7)
										{
											host = "https://www.aira.co.th/i2trade/i2info_AIRA_PLUS_3_0_16_0.ashx";
										}
										else
										{
											if (BrokerId == 10)
											{
												host = "https://trading.aws.co.th/i2trade/i2info_AWS_PLUS_3_0_16_0.ashx";
											}
											else
											{
												if (BrokerId == 89)
												{
													host = "http://www.cns.co.th/i2trade/i2info_CNS_DEMO_PLUS_3_0_15_0.ashx";
												}
												else
												{
													if (BrokerId == 88)
													{
														host = "http://www.i2trade.com/i2trade/i2info_PLUS_3_0_16_0.ashx";
													}
													else
													{
														if (BrokerId == 11)
														{
															host = "https://itrade.cimbsecurities.co.th/i2trade/i2info_CIMB_PLUS_3_0_16_0.ashx";
														}
														else
														{
															if (BrokerId == 12)
															{
																host = "https://sso.kktrade.co.th/i2trade/i2info_KKTRADE_PLUS_3_0_16.ashx";
															}
															else
															{
																if (BrokerId == 13)
																{
																	host = "http://www.lhsec.co.th/i2trade/i2info_LHS_PLUS_3_0_16_0.ashx";
																}
																else
																{
																	if (BrokerId == 14)
																	{
																		host = "http://aecs.com/i2trade/i2info_AEC_PLUS_3_0_16_0.ashx";
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				i2WSResult connectionInfoMulti = i2InfoWS.GetConnectionInfoMulti(host, "S");
				ApplicationInfo.WebUrl = connectionInfoMulti.WsURL;
				ApplicationInfo.WebTfexUrl = connectionInfoMulti.WsDURL;
				if (connectionInfoMulti.WsOrderURL == null)
				{
					ApplicationInfo.WebOrderUrl = connectionInfoMulti.WsURL;
				}
				else
				{
					ApplicationInfo.WebOrderUrl = connectionInfoMulti.WsOrderURL;
				}
				if (connectionInfoMulti.WsUserURL == null)
				{
					ApplicationInfo.WebUserUrl = connectionInfoMulti.WsURL;
				}
				else
				{
					ApplicationInfo.WebUserUrl = connectionInfoMulti.WsUserURL;
				}
				if (connectionInfoMulti.WsAlertURL == null)
				{
					ApplicationInfo.WebAlertUrl = connectionInfoMulti.WsURL;
				}
				else
				{
					ApplicationInfo.WebAlertUrl = connectionInfoMulti.WsAlertURL;
				}
				if (ApplicationInfo.IsSupportTfex)
				{
					if (string.IsNullOrEmpty(ApplicationInfo.WebTfexUrl))
					{
						ApplicationInfo.IsSupportTfex = false;
					}
				}
				else
				{
					ApplicationInfo.WebTfexUrl = string.Empty;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CheckSettingProxy(bool isCheck)
		{
			if (this.chkSettingProxy.InvokeRequired)
			{
				this.chkSettingProxy.Invoke(new frmLogIn.CheckSettingProxyCallBack(this.CheckSettingProxy), new object[]
				{
					isCheck
				});
			}
			else
			{
				this.chkSettingProxy.Checked = isCheck;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReEnableInputPassword(bool isEnable)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmLogIn.ReEnableInputPasswordCallback(this.ReEnableInputPassword), new object[]
				{
					isEnable
				});
			}
			else
			{
				this.txtPassword.Enabled = isEnable;
				this.btnLogin.Enabled = isEnable;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CloseSplashMessage()
		{
			if (this.splashForm.InvokeRequired)
			{
				this.splashForm.Invoke(new MethodInvoker(this.CloseSplashMessage));
			}
			else
			{
				if (this.splashForm != null)
				{
					this.splashForm.Close();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplashMessage(string message)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmLogIn.ShowSplashMessageCallback(this.ShowSplashMessage), new object[]
				{
					message
				});
			}
			else
			{
				this.splashForm.TopLevel = false;
				this.splashForm.Parent = this;
				this.splashForm.TopMost = true;
				this.splashForm.Left = (base.Width - this.splashForm.Width) / 2;
				this.splashForm.Top = this.txtPassword.Top;
				this.splashForm.SetCurrentTask(message);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoading_DoWork(object sender, DoWorkEventArgs e)
		{
			string data = string.Empty;
			try
			{
				this.ShowSplashMessage("Authentication...");
				data = ApplicationInfo.WebUserService.UserAuthen(this._userId, this._userPassword, ApplicationInfo.IP, ApplicationInfo.GetTermicalId(), Application.ProductVersion, true);
				DataSet dataSet = new DataSet();
				MyDataHelper.StringToDataSet(data, dataSet);
				if (dataSet == null || !dataSet.Tables.Contains("Results") || dataSet.Tables["Results"].Rows.Count <= 0)
				{
					this._loginResult = frmLogIn.LoginResult.Fail;
					this._loginResultMessage = "Result not found!!!";
				}
				else
				{
					dataSet.CaseSensitive = false;
					int num;
					int.TryParse(dataSet.Tables["Results"].Rows[0]["code"].ToString(), out num);
					string loginResultMessage = dataSet.Tables["Results"].Rows[0]["message"].ToString();
					if (num < 0)
					{
						if (num == -7)
						{
							this._loginResult = frmLogIn.LoginResult.InvalidVersion;
						}
						else
						{
							this._loginResult = frmLogIn.LoginResult.Fail;
						}
						this._loginResultMessage = loginResultMessage;
					}
					else
					{
						string empty = string.Empty;
						if (dataSet.Tables.Contains("AUTHEN"))
						{
							foreach (DataRow dataRow in dataSet.Tables["AUTHEN"].Rows)
							{
								string text = dataRow["control_name"].ToString();
								switch (text)
								{
								case "session":
									ApplicationInfo.UserSessionID = dataRow["control_value"].ToString();
									break;
								case "account":
									if (!ApplicationInfo.IsOpenFromWeb)
									{
										ApplicationInfo.AccInfo.UserLists = dataRow["Control_value"].ToString().Trim();
									}
									break;
								case "ke_session":
									if (dataRow["Control_value"].ToString() != string.Empty)
									{
										ApplicationInfo.KE_Session = dataRow["Control_value"].ToString();
									}
									break;
								case "ke_sbl":
									ApplicationInfo.KE_SBL = dataRow["Control_value"].ToString();
									break;
								case "ktzmico_session":
									ApplicationInfo.KTZ_Session = dataRow["Control_value"].ToString();
									break;
								case "ktz_cust_map_key":
									ApplicationInfo.KTZ_custMapKey = dataRow["Control_value"].ToString();
									break;
								case "aspticket":
									ApplicationInfo.ASP_Ticket = dataRow["Control_value"].ToString();
									break;
								case "authenkey":
									ApplicationInfo.AuthenKey = dataRow["Control_value"].ToString();
									break;
								case "user_efin":
									if (dataRow["Control_value"].ToString() != string.Empty)
									{
										ApplicationInfo.UserEfin = dataRow["Control_value"].ToString();
									}
									break;
								case "autotrade":
									if (dataRow["Control_value"].ToString() == "Y")
									{
										ApplicationInfo.StopOrderSupported = true;
									}
									else
									{
										if (dataRow["Control_value"].ToString() == "N")
										{
											ApplicationInfo.StopOrderSupported = false;
										}
										else
										{
											this.ShowError("AutoTradeConfig", new Exception(dataRow["Control_value"].ToString()));
										}
									}
                                    //<PS> Enable stop order feature
                                    ApplicationInfo.StopOrderSupported = true;
									break;
								case "eservice_server":
									ApplicationInfo.EserviceServer = dataRow["control_value"].ToString();
									break;
								}
							}
						}
						if (dataSet.Tables.Contains("BrokerControl"))
						{
							foreach (DataRow dataRow in dataSet.Tables["BrokerControl"].Rows)
							{
								string text = dataRow["control_name"].ToString().ToLower().Trim();
								switch (text)
								{
								case "internetuser":
									ApplicationInfo.AccInfo.InternetUser = dataRow["control_value"].ToString();
									break;
								case "internetmutualuser":
									ApplicationInfo.AccInfo.InternetMutualUser = dataRow["control_value"].ToString();
									break;
								case "internetusertfex":
									ApplicationInfo.AccInfo.InternetUserTFEX = dataRow["control_value"].ToString();
									break;
								case "marginrate":
									int.TryParse(dataRow["control_value"].ToString(), out ApplicationInfo.UserMarginRate);
									break;
								case "pincodetimeout":
								{
									int num3;
									int.TryParse(dataRow["control_value"].ToString(), out num3);
									if (num3 > -1)
									{
										ApplicationInfo.UserMaxPincodeTimeout = num3;
									}
									break;
								}
								case "svat":
								{
									decimal num4;
									decimal.TryParse(dataRow["control_value"].ToString(), out num4);
									if (num4 > 0m)
									{
										ApplicationInfo.UserVAT = num4;
									}
									break;
								}
								case "mincustomerpasswordlength":
									int.TryParse(dataRow["control_value"].ToString(), out ApplicationInfo.MinPasswordLength);
									break;
								case "mincustomerpincodelength":
									int.TryParse(dataRow["control_value"].ToString(), out ApplicationInfo.MinPincodeLength);
									break;
								case "customerretrypincode":
									int.TryParse(dataRow["control_value"].ToString(), out ApplicationInfo.UserMaxRetryPincode);
									break;
								case "newslink":
									ApplicationInfo.NewsHomeLink = dataRow["control_value"].ToString();
									break;
								case "newssymbollink":
									ApplicationInfo.NewsSymbolLink = dataRow["control_value"].ToString();
									break;
								case "stockinfolink":
									ApplicationInfo.StockFocusHomeLink = dataRow["control_value"].ToString();
									break;
								case "stockfocussymbollink":
									ApplicationInfo.StockFocusSymbolLink = dataRow["control_value"].ToString();
									break;
								case "clientautogetorder":
									if (dataRow["control_value"].ToString() == "Y")
									{
										ApplicationInfo.IsAutoGetOrderInfo = true;
									}
									else
									{
										ApplicationInfo.IsAutoGetOrderInfo = false;
									}
									break;
								case "clientautogetorderinterval":
								{
									int num3;
									int.TryParse(dataRow["control_value"].ToString(), out num3);
									ApplicationInfo.AutoGetOrderInfoInterval = num3;
									break;
								}
								case "client_sbl":
									ApplicationInfo.SuuportSBL = dataRow["control_value"].ToString();
									break;
								case "client_showsplash":
									ApplicationInfo.SuuportSplash = dataRow["control_value"].ToString();
									break;
								case "ifis":
									ApplicationInfo.SupportFreewill = true;
									break;
								case "supportnav":
									ApplicationInfo.SupportNAV = (dataRow["control_value"].ToString() == "Y");
									break;
								case "supportalert":
									ApplicationInfo.SupportNod = (dataRow["control_value"].ToString() == "Y");
									break;
								case "pullintervalpc":
									int.TryParse(dataRow["control_value"].ToString(), out ApplicationInfo.PullInterval);
									break;
								case "supportcollateral":
									ApplicationInfo.SupportCollateral = dataRow["control_value"].ToString();
									break;
								case "pccaneditorder":
									ApplicationInfo.PCCanEditorder = dataRow["control_value"].ToString();
									break;
								case "supportreorder":
									ApplicationInfo.SupportReOrderBox = (dataRow["control_value"].ToString() == "Y");
									break;
								case "urlefinchart":
									ApplicationInfo.UrlEfinChart = dataRow["control_value"].ToString();
									ApplicationInfo.IsSupportEfinChart = (ApplicationInfo.UrlEfinChart != string.Empty);
									break;
								case "batchorder":
									ApplicationInfo.SupportBatchOrder = dataRow["control_value"].ToString();
									break;
								case "supporttradejournal":
									ApplicationInfo.IsSupportTradeJournal = (dataRow["control_value"].ToString() == "Y");
									break;
								case "supporteservicepc":
									ApplicationInfo.IsSupportEservice = (dataRow["control_value"].ToString() == "Y");
									break;
								case "sendheartbeatmonitor":
									ApplicationInfo.SendHeartBeatMonitor = (dataRow["control_value"].ToString() == "Y");
									break;
								}
							}
						}
						if (dataSet.Tables.Contains("CONFIG"))
						{
							string[] array = new string[]
							{
								"",
								"",
								"",
								"",
								""
							};
							string text2 = string.Empty;
							foreach (DataRow dataRow in dataSet.Tables["CONFIG"].Rows)
							{
								if (!ApplicationInfo.IsSupportTfex)
								{
									if (dataRow["key"].ToString() == "FAV1")
									{
										array[0] = dataRow["value"].ToString().Trim();
									}
									else
									{
										if (dataRow["key"].ToString() == "FAV2")
										{
											array[1] = dataRow["value"].ToString().Trim();
										}
										else
										{
											if (dataRow["key"].ToString() == "FAV3")
											{
												array[2] = dataRow["value"].ToString().Trim();
											}
											else
											{
												if (dataRow["key"].ToString() == "FAV4")
												{
													array[3] = dataRow["value"].ToString().Trim();
												}
												else
												{
													if (dataRow["key"].ToString() == "FAV5")
													{
														array[4] = dataRow["value"].ToString().Trim();
													}
													else
													{
														if (dataRow["key"].ToString() == "TICKER_FIL")
														{
															text2 = dataRow["value"].ToString().Trim();
														}
													}
												}
											}
										}
									}
								}
								else
								{
									if (dataRow["key"].ToString() == "FAV1T")
									{
										array[0] = dataRow["value"].ToString().Trim();
									}
									else
									{
										if (dataRow["key"].ToString() == "FAV2T")
										{
											array[1] = dataRow["value"].ToString().Trim();
										}
										else
										{
											if (dataRow["key"].ToString() == "FAV3T")
											{
												array[2] = dataRow["value"].ToString().Trim();
											}
											else
											{
												if (dataRow["key"].ToString() == "FAV4T")
												{
													array[3] = dataRow["value"].ToString().Trim();
												}
												else
												{
													if (dataRow["key"].ToString() == "FAV5T")
													{
														array[4] = dataRow["value"].ToString().Trim();
													}
													else
													{
														if (dataRow["key"].ToString() == "TICKER_FILT")
														{
															text2 = dataRow["value"].ToString().Trim();
														}
													}
												}
											}
										}
									}
								}
							}
							ApplicationInfo.InitFavStock(array);
							array = null;
							if (text2 != string.Empty)
							{
								string[] array2 = text2.Split(new char[]
								{
									';'
								});
								string[] array3 = array2;
								for (int i = 0; i < array3.Length; i++)
								{
									string item = array3[i];
									ApplicationInfo.TickerStockList.Add(item);
								}
							}
						}
						else
						{
							ApplicationInfo.InitFavStock(null);
						}
						if (string.IsNullOrEmpty(ApplicationInfo.UserSessionID))
						{
							this._loginResult = frmLogIn.LoginResult.Fail;
							this._loginResultMessage = "User Session is empty!!!";
						}
						if (num > 0)
						{
							if (!string.IsNullOrEmpty(ApplicationInfo.AccInfo.UserLists))
							{
								ApplicationInfo.SetupUsers(ApplicationInfo.AccInfo.UserLists, '/');
								ApplicationInfo.UserLoginID = this.txtUserID.Text.Trim();
							}
							this._loginResult = frmLogIn.LoginResult.Success;
						}
					}
				}
				dataSet.Clear();
				dataSet = null;
			}
			catch (Exception ex)
			{
				this._loginResult = frmLogIn.LoginResult.Fail;
				this._loginResultMessage = ex.Message;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoading_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this._loginResult == frmLogIn.LoginResult.InvalidVersion)
				{
					DialogResult dialogResult = MessageBox.Show(this._loginResultMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
					if (dialogResult == DialogResult.Yes)
					{
						Process.Start(ApplicationInfo.WebService.GetUrlClient());
					}
					if (this.splashForm != null)
					{
						this.splashForm.Close();
					}
					this.txtPassword.Enabled = true;
					this.btnLogin.Enabled = true;
					return;
				}
				if (this._loginResult == frmLogIn.LoginResult.Fail)
				{
					if (this.splashForm != null)
					{
						this.splashForm.Close();
					}
					if (this._loginResultMessage != string.Empty)
					{
						MessageBox.Show(this._loginResultMessage);
					}
					else
					{
						MessageBox.Show("Unknow error!!!");
					}
					this.txtPassword.Enabled = true;
					this.btnLogin.Enabled = true;
					this.txtUserID.Focus();
					this.txtUserID.SelectAll();
					return;
				}
				if (this.splashForm != null)
				{
					this.splashForm.Close();
				}
				this.txtUserID.Text = string.Empty;
				this.txtPassword.Text = string.Empty;
				this.txtPassword.Enabled = true;
				this.btnLogin.Enabled = true;
				this.txtUserID.Focus();
				this.txtUserID.SelectAll();
				this.chkSettingProxy.Checked = false;
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoading_RunWorkerCompleted", ex);
			}
			base.Hide();
			try
			{
				if (this.frm == null)
				{
					this.frm = new frmMain();
				}
				this.frm.FormClosed -= new FormClosedEventHandler(this.frmMain_FormClosed);
				this.frm.FormClosed += new FormClosedEventHandler(this.frmMain_FormClosed);
				this.frm.Show();
				if (this.splashForm != null)
				{
					this.splashForm.Close();
					this.splashForm.Dispose();
				}
				this.splashForm = null;
			}
			catch (Exception ex)
			{
				EventLog.WriteEntry("i2Trade", ex.Message, EventLogEntryType.Error);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Thread.Sleep(500);
			if (this.frm != null)
			{
				if (!this.frm.IsDisposed)
				{
					this.frm.Dispose();
				}
				this.frm = null;
			}
			GC.Collect();
			Application.Exit();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtUserID_Enter(object sender, EventArgs e)
		{
			this.txtUserID.BackColor = Color.LightYellow;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtPassword_Enter(object sender, EventArgs e)
		{
			this.txtPassword.BackColor = Color.LightYellow;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtPassword_Leave(object sender, EventArgs e)
		{
			this.txtPassword.BackColor = Color.White;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtUserID_Leave(object sender, EventArgs e)
		{
			this.txtUserID.BackColor = Color.White;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string methodName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, methodName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenProxySetting()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.OpenProxySetting));
			}
			else
			{
				try
				{
					base.Height = (this.chkSettingProxy.Checked ? (this.pnLogin.Height + this.gbProxy.Height + 5) : this.pnLogin.Height);
					this.chkRememberPwd.Checked = Settings.Default.RememberProxyPassword;
					if (!string.IsNullOrEmpty(Settings.Default.ProxyHost))
					{
						this.txtProxyHost.Text = Settings.Default.ProxyHost;
						this.txtProxyPort.Text = Settings.Default.ProxyPort.ToString();
					}
					else
					{
						string registryStringValue = BusinessServiceFactory.GetRegistryStringValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyServer");
						if (!string.IsNullOrEmpty(registryStringValue))
						{
							string[] array = registryStringValue.Split(new char[]
							{
								':'
							});
							if (array != null && array.Length > 1)
							{
								this.txtProxyHost.Text = array[0];
								this.txtProxyPort.Text = array[1];
							}
						}
					}
					this.txtProxyUserName.Text = Settings.Default.ProxyUsername;
					this.txtProxyPassword.Text = Settings.Default.ProxyPassword;
					this.txtProxyUserName.Focus();
				}
				catch (Exception ex)
				{
					this.ShowError("OpenProxySetting", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaveProxySetting()
		{
			try
			{
				ApplicationInfo.IsUseProxy = true;
				Settings.Default.ProxyHost = this.txtProxyHost.Text;
				Settings.Default.ProxyPort = (string.IsNullOrEmpty(this.txtProxyPort.Text) ? 80 : Convert.ToInt32(this.txtProxyPort.Text));
				Settings.Default.RememberProxyPassword = this.chkRememberPwd.Checked;
				Settings.Default.ProxyUsername = this.txtProxyUserName.Text;
				ApplicationInfo.ProxyPassword = this.txtProxyPassword.Text;
				if (this.chkRememberPwd.Checked)
				{
					Settings.Default.ProxyPassword = this.txtProxyPassword.Text;
				}
				else
				{
					Settings.Default.ProxyPassword = string.Empty;
				}
				Settings.Default.Save();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void chkSetingProxy_CheckedChanged(object sender, EventArgs e)
		{
			this.gbProxy.Top = 253;
			this.gbProxy.Visible = this.chkSettingProxy.Checked;
			base.Height = (this.chkSettingProxy.Checked ? (this.pnLogin.Height + this.gbProxy.Height + 10) : (this.pnLogin.Top * 2 + this.pnLogin.Height));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtProxyPassword_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.btnLogin.PerformClick();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmLogIn_Shown(object sender, EventArgs e)
		{
			if (ApplicationInfo.IsOpenFromWeb)
			{
				this.txtUserID.Text = ApplicationInfo.UserLoginID;
				if (this.txtUserID.Text != string.Empty)
				{
					this.txtPassword.Focus();
				}
				else
				{
					this.txtUserID.Focus();
				}
				this.txtUserID.Enabled = !ApplicationInfo.IsOpenFromWeb;
				this.lbPassword.Hide();
				this.txtPassword.Hide();
				if (this.CheckValidateRequireField())
				{
					this.LoginProcessing();
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(Settings.Default.LastLoginId))
				{
					this.txtUserID.Text = Settings.Default.LastLoginId;
					this.txtPassword.Focus();
				}
				else
				{
					this.txtUserID.Focus();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void pnLogin_Paint(object sender, PaintEventArgs e)
		{
			if (e.ClipRectangle.Width == this.pnLogin.ClientSize.Width)
			{
				e.Graphics.DrawRectangle(Pens.Gainsboro, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
			}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmLogIn));
			this.lbCopyRight = new Label();
			this.txtPassword = new TextBox();
			this.txtUserID = new TextBox();
			this.btnCancel = new Button();
			this.btnLogin = new Button();
			this.lbUserID = new Label();
			this.lbPassword = new Label();
			this.pnLogin = new Panel();
			this.chbSupportTfex = new CheckBox();
			this.pictureBox1 = new PictureBox();
			this.chkSettingProxy = new CheckBox();
			this.label1 = new Label();
			this.chkRememberPwd = new CheckBox();
			this.txtProxyPort = new TextBox();
			this.txtProxyHost = new TextBox();
			this.txtProxyPassword = new TextBox();
			this.txtProxyUserName = new TextBox();
			this.label4 = new Label();
			this.label5 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.gbProxy = new GroupBox();
			this.lblNote = new Label();
			this.pnLogin.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.gbProxy.SuspendLayout();
			base.SuspendLayout();
			this.lbCopyRight.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.lbCopyRight.BackColor = Color.Transparent;
			this.lbCopyRight.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbCopyRight.ForeColor = Color.LightGray;
			this.lbCopyRight.Location = new Point(3, 218);
			this.lbCopyRight.Name = "lbCopyRight";
			this.lbCopyRight.Size = new Size(449, 22);
			this.lbCopyRight.TabIndex = 15;
			this.lbCopyRight.Text = "Copyright 2008 STI Revolution Co.,Ltd  All rights reserved";
			this.lbCopyRight.TextAlign = ContentAlignment.BottomCenter;
			this.lbCopyRight.UseCompatibleTextRendering = true;
			this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
			this.txtPassword.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.txtPassword.Location = new Point(182, 109);
			this.txtPassword.MaxLength = 16;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new Size(170, 27);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtPassword.KeyDown += new KeyEventHandler(this.txtPassword_KeyDown);
			this.txtPassword.Leave += new EventHandler(this.txtPassword_Leave);
			this.txtPassword.Enter += new EventHandler(this.txtPassword_Enter);
			this.txtUserID.BackColor = Color.White;
			this.txtUserID.BorderStyle = BorderStyle.FixedSingle;
			this.txtUserID.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.txtUserID.Location = new Point(182, 79);
			this.txtUserID.MaxLength = 50;
			this.txtUserID.Name = "txtUserID";
			this.txtUserID.Size = new Size(170, 27);
			this.txtUserID.TabIndex = 1;
			this.txtUserID.KeyDown += new KeyEventHandler(this.txtUserID_KeyDown);
			this.txtUserID.Leave += new EventHandler(this.txtUserID_Leave);
			this.txtUserID.Enter += new EventHandler(this.txtUserID_Enter);
			this.btnCancel.AutoSize = true;
			this.btnCancel.BackColor = SystemColors.Info;
			this.btnCancel.Cursor = Cursors.Arrow;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.FlatStyle = FlatStyle.Flat;
			this.btnCancel.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnCancel.Location = new Point(245, 174);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(81, 31);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnLogin.AutoSize = true;
			this.btnLogin.BackColor = SystemColors.Info;
			this.btnLogin.Cursor = Cursors.Arrow;
			this.btnLogin.FlatStyle = FlatStyle.Flat;
			this.btnLogin.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnLogin.Location = new Point(141, 174);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new Size(81, 31);
			this.btnLogin.TabIndex = 3;
			this.btnLogin.Text = "Ok";
			this.btnLogin.UseVisualStyleBackColor = false;
			this.btnLogin.Click += new EventHandler(this.btnOk_Click);
			this.lbUserID.AutoSize = true;
			this.lbUserID.BackColor = Color.Transparent;
			this.lbUserID.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbUserID.ForeColor = Color.LightGray;
			this.lbUserID.Location = new Point(92, 82);
			this.lbUserID.Name = "lbUserID";
			this.lbUserID.Size = new Size(93, 19);
			this.lbUserID.TabIndex = 9;
			this.lbUserID.Text = "Username :";
			this.lbPassword.AutoSize = true;
			this.lbPassword.BackColor = Color.Transparent;
			this.lbPassword.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbPassword.ForeColor = Color.LightGray;
			this.lbPassword.Location = new Point(95, 113);
			this.lbPassword.Name = "lbPassword";
			this.lbPassword.Size = new Size(90, 19);
			this.lbPassword.TabIndex = 10;
			this.lbPassword.Text = "Password :";
			this.pnLogin.BackColor = Color.Transparent;
			this.pnLogin.BorderStyle = BorderStyle.FixedSingle;
			this.pnLogin.Controls.Add(this.chbSupportTfex);
			this.pnLogin.Controls.Add(this.pictureBox1);
			this.pnLogin.Controls.Add(this.chkSettingProxy);
			this.pnLogin.Controls.Add(this.label1);
			this.pnLogin.Controls.Add(this.lbCopyRight);
			this.pnLogin.Controls.Add(this.txtPassword);
			this.pnLogin.Controls.Add(this.txtUserID);
			this.pnLogin.Controls.Add(this.lbUserID);
			this.pnLogin.Controls.Add(this.btnCancel);
			this.pnLogin.Controls.Add(this.lbPassword);
			this.pnLogin.Controls.Add(this.btnLogin);
			this.pnLogin.Cursor = Cursors.Default;
			this.pnLogin.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.pnLogin.Location = new Point(0, 0);
			this.pnLogin.Name = "pnLogin";
			this.pnLogin.Size = new Size(457, 249);
			this.pnLogin.TabIndex = 20;
			this.pnLogin.Paint += new PaintEventHandler(this.pnLogin_Paint);
			this.chbSupportTfex.AutoSize = true;
			this.chbSupportTfex.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chbSupportTfex.ForeColor = Color.FromArgb(255, 128, 0);
			this.chbSupportTfex.Location = new Point(139, 145);
			this.chbSupportTfex.Name = "chbSupportTfex";
			this.chbSupportTfex.Size = new Size(122, 21);
			this.chbSupportTfex.TabIndex = 25;
			this.chbSupportTfex.Text = "Support TFEX";
			this.chbSupportTfex.UseVisualStyleBackColor = true;
			this.pictureBox1.Image = (Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(135, 6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(203, 46);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 24;
			this.pictureBox1.TabStop = false;
			this.chkSettingProxy.AutoSize = true;
			this.chkSettingProxy.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chkSettingProxy.ForeColor = Color.LightGray;
			this.chkSettingProxy.Location = new Point(262, 145);
			this.chkSettingProxy.Name = "chkSettingProxy";
			this.chkSettingProxy.Size = new Size(116, 21);
			this.chkSettingProxy.TabIndex = 23;
			this.chkSettingProxy.Text = "Setting Proxy";
			this.chkSettingProxy.UseVisualStyleBackColor = true;
			this.chkSettingProxy.CheckedChanged += new EventHandler(this.chkSetingProxy_CheckedChanged);
			this.label1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.label1.BackColor = Color.Transparent;
			this.label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label1.ForeColor = Color.LightGray;
			this.label1.Location = new Point(11, 54);
			this.label1.Name = "label1";
			this.label1.Size = new Size(437, 21);
			this.label1.TabIndex = 22;
			this.label1.Text = "Internet Trading Workstation";
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			this.label1.UseCompatibleTextRendering = true;
			this.chkRememberPwd.AutoSize = true;
			this.chkRememberPwd.Location = new Point(260, 140);
			this.chkRememberPwd.Name = "chkRememberPwd";
			this.chkRememberPwd.Size = new Size(192, 24);
			this.chkRememberPwd.TabIndex = 8;
			this.chkRememberPwd.Text = "Remember Password";
			this.chkRememberPwd.UseVisualStyleBackColor = true;
			this.txtProxyPort.BorderStyle = BorderStyle.FixedSingle;
			this.txtProxyPort.Location = new Point(260, 52);
			this.txtProxyPort.Name = "txtProxyPort";
			this.txtProxyPort.Size = new Size(125, 26);
			this.txtProxyPort.TabIndex = 7;
			this.txtProxyHost.BorderStyle = BorderStyle.FixedSingle;
			this.txtProxyHost.Location = new Point(260, 24);
			this.txtProxyHost.Name = "txtProxyHost";
			this.txtProxyHost.Size = new Size(125, 26);
			this.txtProxyHost.TabIndex = 6;
			this.txtProxyPassword.BorderStyle = BorderStyle.FixedSingle;
			this.txtProxyPassword.Location = new Point(261, 106);
			this.txtProxyPassword.Name = "txtProxyPassword";
			this.txtProxyPassword.PasswordChar = '*';
			this.txtProxyPassword.Size = new Size(125, 26);
			this.txtProxyPassword.TabIndex = 9;
			this.txtProxyPassword.KeyUp += new KeyEventHandler(this.txtProxyPassword_KeyUp);
			this.txtProxyUserName.BorderStyle = BorderStyle.FixedSingle;
			this.txtProxyUserName.Location = new Point(261, 78);
			this.txtProxyUserName.Name = "txtProxyUserName";
			this.txtProxyUserName.Size = new Size(125, 26);
			this.txtProxyUserName.TabIndex = 8;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(179, 108);
			this.label4.Name = "label4";
			this.label4.Size = new Size(83, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "Password";
			this.label5.AutoSize = true;
			this.label5.Location = new Point(170, 81);
			this.label5.Name = "label5";
			this.label5.Size = new Size(94, 20);
			this.label5.TabIndex = 2;
			this.label5.Text = "User Name";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(215, 54);
			this.label3.Name = "label3";
			this.label3.Size = new Size(40, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Port";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(188, 27);
			this.label2.Name = "label2";
			this.label2.Size = new Size(71, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Address";
			this.gbProxy.BackColor = SystemColors.Control;
			this.gbProxy.Controls.Add(this.txtProxyPassword);
			this.gbProxy.Controls.Add(this.lblNote);
			this.gbProxy.Controls.Add(this.label4);
			this.gbProxy.Controls.Add(this.txtProxyUserName);
			this.gbProxy.Controls.Add(this.chkRememberPwd);
			this.gbProxy.Controls.Add(this.label2);
			this.gbProxy.Controls.Add(this.label5);
			this.gbProxy.Controls.Add(this.txtProxyPort);
			this.gbProxy.Controls.Add(this.label3);
			this.gbProxy.Controls.Add(this.txtProxyHost);
			this.gbProxy.Cursor = Cursors.Default;
			this.gbProxy.Location = new Point(7, 0);
			this.gbProxy.Name = "gbProxy";
			this.gbProxy.Size = new Size(442, 173);
			this.gbProxy.TabIndex = 21;
			this.gbProxy.TabStop = false;
			this.gbProxy.Text = "Proxy Setting";
			this.gbProxy.Visible = false;
			this.lblNote.BackColor = Color.FromArgb(255, 255, 192);
			this.lblNote.Location = new Point(6, 22);
			this.lblNote.Name = "lblNote";
			this.lblNote.Size = new Size(146, 147);
			this.lblNote.TabIndex = 10;
			this.lblNote.Text = "\r\nคำแนะนำ\r\n   ในกรณีที่ต้องมีการเชื่อมต่อ \r\n Internet ผ่าน Proxy\r\n ท่านสามารถสอบถามข้อมูล\r\n เกี่ยวกับการใช้งาน Proxy \r\n ได้จาก ผู้ดูแลระบบของท่าน";
			base.AutoScaleDimensions = new SizeF(9f, 20f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = Color.FromArgb(50, 50, 50);
			base.ClientSize = new Size(457, 249);
			base.ControlBox = false;
			base.Controls.Add(this.pnLogin);
			base.Controls.Add(this.gbProxy);
			this.Cursor = Cursors.NoMove2D;
			this.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "frmLogIn";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "i2Trade Plus";
			base.Load += new EventHandler(this.frmLogIn_Load);
			base.Shown += new EventHandler(this.frmLogIn_Shown);
			this.pnLogin.ResumeLayout(false);
			this.pnLogin.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.gbProxy.ResumeLayout(false);
			this.gbProxy.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
