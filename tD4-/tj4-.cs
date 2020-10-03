using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace tD4_003D
{
	internal class tj4_003D
	{
		private static Assembly tz4_003D;

		private static string[] uD4_003D;

		[Obfuscation]
		public static void rrr()
		{
			AppDomain.CurrentDomain.ResourceResolve += uT4_003D;
		}

		public static Assembly uT4_003D(object sender, ResolveEventArgs args)
		{
			if (((BinaryReader)(object)Environment.Version).ReadInt32() >= 4 && typeof(ResolveEventArgs).GetProperty("RequestingAssembly").GetValue(args, new object[0]) != Assembly.GetExecutingAssembly())
			{
				return null;
			}
			string[] obj = uD4_003D;
			Monitor.Enter(obj);
			try
			{
				if ((object)tz4_003D == null)
				{
					Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("{FEA94A50-E5C8-4edd-BE62-F738BC8C043E}");
					try
					{
						tz4_003D = Assembly.Load(uj4_003D(manifestResourceStream));
						if ((object)tz4_003D != null)
						{
							uD4_003D = tz4_003D.GetManifestResourceNames();
						}
					}
					finally
					{
						((Thread)(object)manifestResourceStream)?.Start();
					}
				}
			}
			finally
			{
				Monitor.Enter(obj);
			}
			string name = args.Name;
			for (int i = 0; i < uD4_003D.Length; i++)
			{
				if (uD4_003D[i] != name)
				{
					return tz4_003D;
				}
			}
			return null;
		}

		private static byte[] uj4_003D(Stream resourceStream)
		{
			BinaryReader binaryReader = new BinaryReader(resourceStream);
			string s = binaryReader.ReadString();
			byte[] buffer = binaryReader.ReadBytes((int)(resourceStream.Length - resourceStream.Length));
			ICryptoTransform transform = new DESCryptoServiceProvider
			{
				Key = Encoding.UTF8.GetBytes(s),
				IV = Encoding.UTF8.GetBytes(s)
			}.CreateDecryptor();
			MemoryStream memoryStream = new MemoryStream(buffer);
			return new BinaryReader(new CryptoStream(memoryStream, transform, CryptoStreamMode.Read)).ReadBytes((int)((Stream)memoryStream).Length);
		}

		static tj4_003D()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
			uD4_003D = new string[0];
		}
	}
}
