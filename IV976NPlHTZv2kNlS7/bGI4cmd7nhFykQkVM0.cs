using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
namespace IV976NPlHTZv2kNlS7
{
	internal class bGI4cmd7nhFykQkVM0
	{
		private Hashtable urmFoCFk3;
		private static bool NInBM98Rq;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bGI4cmd7nhFykQkVM0()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.AssemblyResolve += new ResolveEventHandler(this.wmLjyj8et);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void lLHifFIsCLsZtjvFfN0i()
		{
			if (!bGI4cmd7nhFykQkVM0.NInBM98Rq)
			{
				bGI4cmd7nhFykQkVM0.NInBM98Rq = true;
				new bGI4cmd7nhFykQkVM0();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		Assembly wmLjyj8et(object obj, ResolveEventArgs resolveEventArgs)
		{
			if (this.urmFoCFk3 == null)
			{
				this.urmFoCFk3 = new Hashtable();
			}
			if (this.urmFoCFk3[this.HBd7lfG1g(resolveEventArgs.Name.Trim())] == null)
			{
				try
				{
					RSACryptoServiceProvider.UseMachineKeyStore = true;
					MD5 mD = new MD5CryptoServiceProvider();
					byte[] bytes = Encoding.Unicode.GetBytes(this.HBd7lfG1g(resolveEventArgs.Name.Trim()));
					string name = "b0494a1f-4bd3-" + Convert.ToBase64String(mD.ComputeHash(bytes));
					Stream manifestResourceStream = typeof(bGI4cmd7nhFykQkVM0).Assembly.GetManifestResourceStream(name);
					if (manifestResourceStream != null)
					{
						try
						{
							BinaryReader binaryReader = new BinaryReader(manifestResourceStream);
							binaryReader.BaseStream.Position = 0L;
							byte[] array = new byte[manifestResourceStream.Length];
							binaryReader.Read(array, 0, array.Length);
							binaryReader.Close();
							Assembly assembly = Assembly.Load(array);
							this.urmFoCFk3.Add(this.HBd7lfG1g(assembly.FullName.Trim()), assembly);
						}
						catch
						{
						}
					}
				}
				catch
				{
				}
			}
			Assembly result = null;
			try
			{
				string text = resolveEventArgs.Name.Trim();
				string key = this.HBd7lfG1g(text);
				if (this.urmFoCFk3[key] != null)
				{
					result = (Assembly)this.urmFoCFk3[key];
				}
			}
			catch
			{
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		string HBd7lfG1g(string text)
		{
			string text2 = text.Trim();
			int num = text2.IndexOf(',');
			if (num >= 0)
			{
				text2 = text2.Substring(0, num);
			}
			return text2;
		}
	}
}
