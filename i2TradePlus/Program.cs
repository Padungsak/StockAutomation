using i2TradePlus.Properties;
using ITSNet.CefBrowser;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
namespace i2TradePlus
{
	public static class Program
	{
		private const int SW_RESTORE = 9;
		private static Mutex mutex;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Start(params string[] args)
		{
			Program.Main(args);
		}
		[STAThread]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Main(params string[] args)
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				string empty = string.Empty;
				string empty2 = string.Empty;
				string empty3 = string.Empty;
				string empty4 = string.Empty;
				string empty5 = string.Empty;
				string empty6 = string.Empty;
				string empty7 = string.Empty;
				string empty8 = string.Empty;
				if (args != null && args.Length > 0)
				{
					string[] array = args[0].Split(new char[]
					{
						';'
					});
					string[] array2 = array;
					int i = 0;
					while (i < array2.Length)
					{
						string text = array2[i];
						string[] array3 = text.Split(new char[]
						{
							'='
						});
						string text2 = array3[0].ToLower();
						switch (text2)
						{
						case "openfromweb":
							bool.TryParse(array3[1].ToString(), out ApplicationInfo.IsOpenFromWeb);
							break;
						case "brokerid":
							int.TryParse(array3[1].ToString(), out ApplicationInfo.BrokerId);
							break;
						case "appid":
							ApplicationInfo.UserLoginMode = array3[1];
							break;
						case "inbroker":
							ApplicationInfo.AccInfo.UserInternetInBroker = array3[1];
							break;
						case "account":
							ApplicationInfo.AccInfo.UserLists = array3[1];
							break;
						case "usernameproxy":
							Settings.Default.ProxyUsername = array3[1];
							break;
						case "passwordproxy":
							ApplicationInfo.ProxyPassword = array3[1];
							break;
						case "key":
							ApplicationInfo.AuthenKey = array3[1];
							break;
						case "pin":
							ApplicationInfo.UserPincode = array3[1];
							break;
						case "ke_session":
							ApplicationInfo.KE_Session = array3[1];
							break;
						case "ke_athurl":
							ApplicationInfo.KE_AuthenUrl = array3[1];
							break;
						case "ke_local":
							ApplicationInfo.KE_LOCAL = array3[1];
							break;
						case "aspticket":
							ApplicationInfo.ASP_Ticket = array3[1];
							break;
						case "ktz_session":
							ApplicationInfo.KTZ_Session = array3[1];
							break;
						case "ktz_cust_map_key":
							ApplicationInfo.KTZ_custMapKey = array3[1];
							break;
						case "user_efin":
							ApplicationInfo.UserEfin = array3[1];
							break;
						case "req_tfex":
							Settings.Default.RequestTfex = (array3[1] == "Y");
							break;
						case "second_i2info":
							ApplicationInfo.Isi2infoLink2 = (array3[1] == "Y");
							break;
						}
						//IL_380:
						i++;
						continue;
						//goto IL_380;
					}
					if (ApplicationInfo.IsOpenFromWeb)
					{
						if (ApplicationInfo.AccInfo.UserLists != string.Empty)
						{
							string[] array4 = ApplicationInfo.AccInfo.UserLists.Split(new char[]
							{
								'|'
							});
							if (array4.Length > 0)
							{
								ApplicationInfo.UserLoginID = array4[0];
								ApplicationInfo.AccInfo.UserLists = ApplicationInfo.AccInfo.UserLists.Replace('|', '/');
							}
						}
					}
					Program.InitCefWeb();
					Application.Run(new frmLogIn());
				}
			}
			catch (Exception ex)
			{
				EventLog.WriteEntry("i2TradePlus", ex.Message);
			}
			finally
			{
			}
			try
			{
				CefRuntime.Shutdown();
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void InitCefWeb()
		{
			try
			{
				try
				{
					CefRuntime.Load();
				}
				catch (DllNotFoundException ex)
				{
					MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				catch (CefRuntimeException ex2)
				{
					MessageBox.Show(ex2.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				catch (Exception ex3)
				{
					MessageBox.Show(ex3.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				string[] args = new string[0];
				CefMainArgs args2 = new CefMainArgs(args);
				MyCefApp application = new MyCefApp();
				int num = CefRuntime.ExecuteProcess(args2, application);
				if (num == -1)
				{
					CefSettings cefSettings = new CefSettings();
					cefSettings.SingleProcess = false;
					cefSettings.MultiThreadedMessageLoop = true;
					cefSettings.LogSeverity = CefLogSeverity.Disable;
					cefSettings.LogFile = "CefBrowser.log";
					CefRuntime.Initialize(args2, cefSettings, application);
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					if (!cefSettings.MultiThreadedMessageLoop)
					{
						Application.Idle += new EventHandler(Program.Application_Idle);
					}
				}
			}
			catch (Exception ex4)
			{
				MessageBox.Show(ex4.ToString(), "Init CefChart", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Application_Idle(object sender, EventArgs e)
		{
			CefRuntime.DoMessageLoopWork();
		}
		[DllImport("user32.dll")]
		private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
		[DllImport("user32.dll")]
		private static extern int SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll")]
		private static extern int IsIconic(IntPtr hWnd);
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IntPtr GetCurrentInstanceWindowHandle()
		{
			IntPtr result = IntPtr.Zero;
			Process currentProcess = Process.GetCurrentProcess();
			Process[] processesByName = Process.GetProcessesByName(currentProcess.ProcessName);
			Process[] array = processesByName;
			for (int i = 0; i < array.Length; i++)
			{
				Process process = array[i];
				if (process.Id != currentProcess.Id && process.MainModule.FileName == currentProcess.MainModule.FileName && process.MainWindowHandle != IntPtr.Zero)
				{
					result = process.MainWindowHandle;
					break;
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void SwitchToCurrentInstance()
		{
			IntPtr currentInstanceWindowHandle = Program.GetCurrentInstanceWindowHandle();
			if (currentInstanceWindowHandle != IntPtr.Zero)
			{
				if (Program.IsIconic(currentInstanceWindowHandle) != 0)
				{
					Program.ShowWindow(currentInstanceWindowHandle, 9);
				}
				Program.SetForegroundWindow(currentInstanceWindowHandle);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool Run(Form frmMain)
		{
			bool result;
			if (Program.IsAlreadyRunning())
			{
				Program.SwitchToCurrentInstance();
				result = false;
			}
			else
			{
				Application.Run(frmMain);
				result = true;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool Run()
		{
			return !Program.IsAlreadyRunning();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool IsAlreadyRunning()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			FileSystemInfo fileSystemInfo = new FileInfo(location);
			string name = fileSystemInfo.Name;
			bool flag;
			Program.mutex = new Mutex(true, "Global\\" + name, out flag);
			if (flag)
			{
				Program.mutex.ReleaseMutex();
			}
			return !flag;
		}
	}
}
