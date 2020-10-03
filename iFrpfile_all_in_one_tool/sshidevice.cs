using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace iFrpfile_all_in_one_tool
{
	public class sshidevice
	{
		private libidevice oRM_003D = new libidevice();

		public static SshClient sshclient;

		public string ERROR = "";

		public string SSH_Command(string Command)
		{
			sshclient = new SshClient("127.0.0.1", "root", "alpine");
			string text = "";
			try
			{
				((Thread)(object)sshclient).Start();
				SshCommand sshCommand = sshclient.CreateCommand(Command);
				IAsyncResult asyncResult = sshCommand.BeginExecute();
				while (!asyncResult.IsCompleted)
				{
					Thread.Sleep(200);
				}
				text = sshCommand.EndExecute(asyncResult);
				((Thread)(object)sshclient).Start();
			}
			catch (Exception ex)
			{
				ERROR = ((TextReader)(object)ex).ReadToEnd();
				text = "Failed";
			}
			return text;
		}

		public string SPC_UploadFile(string strfile, string path)
		{
			string text = "Done";
			try
			{
				ScpClient scpClient = new ScpClient("127.0.0.1", "root", "alpine");
				try
				{
					((Thread)(object)scpClient).Start();
					scpClient.Upload(new FileInfo(strfile), path);
					((Thread)(object)scpClient).Start();
				}
				finally
				{
					((Thread)(object)scpClient)?.Start();
				}
			}
			catch (Exception ex)
			{
				text = "ERROR: " + ((TextReader)(object)ex).ReadToEnd();
			}
			ERROR = text;
			return text;
		}

		public string SPC_UploadFolder(string strfile, string path)
		{
			string result = "Done";
			try
			{
				ScpClient scpClient = new ScpClient("127.0.0.1", "root", "alpine");
				try
				{
					((Thread)(object)scpClient).Start();
					scpClient.Upload(new DirectoryInfo(strfile), path);
					((Thread)(object)scpClient).Start();
				}
				finally
				{
					((Thread)(object)scpClient)?.Start();
				}
			}
			catch (Exception ex)
			{
				result = "ERROR: " + ((TextReader)(object)ex).ReadToEnd();
			}
			return result;
		}

		public bool SPC_DownloadFile(string strfile, string path)
		{
			bool result = false;
			try
			{
				string fileName = path + Path.GetFileName(strfile);
				ScpClient scpClient = new ScpClient("127.0.0.1", "root", "alpine");
				try
				{
					((Thread)(object)scpClient).Start();
					scpClient.Download(strfile, new FileInfo(fileName));
					((Thread)(object)scpClient).Start();
					result = true;
				}
				finally
				{
					((Thread)(object)scpClient)?.Start();
				}
			}
			catch (Exception ex)
			{
				ERROR = ((TextReader)(object)ex).ReadToEnd();
			}
			return result;
		}

		public void SPC_DownloadFolder(string strfile, string path)
		{
			try
			{
				string path2 = path + Path.GetFileName(strfile);
				ScpClient scpClient = new ScpClient("127.0.0.1", "root", "alpine");
				try
				{
					((Thread)(object)scpClient).Start();
					scpClient.Download(strfile, new DirectoryInfo(path2));
					((Thread)(object)scpClient).Start();
				}
				finally
				{
					((Thread)(object)scpClient)?.Start();
				}
			}
			catch (Exception ex)
			{
				ERROR = ((TextReader)(object)ex).ReadToEnd();
			}
		}

		public string SSH_GetSerialNumber()
		{
			string result = "";
			try
			{
				result = SSH_Command("ioreg -l | grep IOPlatformSerialNumber --color=never").Replace("\"", "").Replace("\t", "").Replace(" ", "")
					.Replace("|IOPlatformSerialNumber=", "")
					.Trim()
					.Remove(0, 13);
			}
			catch
			{
			}
			return result;
		}

		public string SSH_GetModelName()
		{
			string text = SSH_Command("ioreg -l | grep product-name --color=never").Replace("\"", "").Replace("\t", "").Replace(" ", "")
				.Replace("|product-name=", "");
			int num = text.IndexOf("<i");
			string text2 = text.Substring(num + 1);
			return text2.Substring(0, text2.Length - 2);
		}

		public string get_IOS()
		{
			string result = "";
			List<string> list = new List<string>();
			try
			{
				string path = Application.StartupPath + "\\Resources\\Data\\UDID\\" + oRM_003D.getUDID();
				Directory.CreateDirectory(path);
				SPC_DownloadFile("/System/Library/CoreServices/SystemVersion.plist", Application.StartupPath + "\\Resources\\Data\\UDID\\" + oRM_003D.getUDID() + "\\");
				string[] array = File.ReadAllLines(Application.StartupPath + "\\Resources\\Data\\UDID\\" + oRM_003D.getUDID() + "\\SystemVersion.plist");
				foreach (string item in array)
				{
					list.Add(item);
				}
				string text = "null";
				string text2 = "null";
				for (int j = 0; j < list.Count; j++)
				{
					string text3 = list[j];
					if (text3.Contains("ProductVersion"))
					{
						text = list[j + 1];
						text2 = text.Substring(9);
						result = text2.Substring(0, text2.Length - 9);
						Directory.Delete(Application.StartupPath + "\\Resources\\Data\\UDID\\", recursive: true);
					}
				}
			}
			catch (Exception ex)
			{
				ERROR = ((TextReader)(object)ex).ReadToEnd();
			}
			return result;
		}

		public bool install_iproxy()
		{
			bool result = false;
			try
			{
				Process process = new Process();
				ProcessStartInfo processStartInfo2 = process.StartInfo = new ProcessStartInfo
				{
					FileName = oRM_003D.get_pathlibidevice() + "\\iproxy.exe",
					Arguments = "22 44 " + oRM_003D.getUDID(),
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				};
				process.Start();
				result = true;
			}
			catch (Exception ex)
			{
				ERROR = ((TextReader)(object)ex).ReadToEnd();
			}
			return result;
		}

		public bool iErase()
		{
			bool result = true;
			try
			{
				SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\erase", "/bin/erase");
				SSH_Command("chmod -R 777 /bin");
				SSH_Command("killall -9 backboardd");
				SSH_Command("erase -command da7e6b6d2c20eb316c093");
				SSH_Command("launchctl load -w /System/Library/LaunchDaemons/com.apple.mobile.obliteration.plist");
			}
			catch (Exception ex)
			{
				result = false;
				ERROR = ((TextReader)(object)ex).ReadToEnd();
			}
			return result;
		}

		public void StartIproxy()
		{
			try
			{
				Process process = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						FileName = oRM_003D.get_pathlibidevice() + "\\iproxy.exe",
						Arguments = "22 44",
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true
					}
				};
				process.Start();
			}
			catch (Win32Exception)
			{
				MessageBox.Show("iproxy not found");
			}
		}

		public void ldrestart2()
		{
			SshClient sshClient = new SshClient("127.0.0.1", "root", "alpine");
			try
			{
				sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(5.0);
				((Thread)(object)sshClient).Start();
				((TextReader)(object)sshClient.CreateCommand("ldrestart && ldrestart")).ReadToEnd();
				Thread.Sleep(5000);
				((Thread)(object)sshClient).Start();
				Process[] processesByName = Process.GetProcessesByName("iproxy");
				foreach (Process process in processesByName)
				{
					((Thread)(object)process).Start();
				}
				StartIproxy();
				if (File.Exists("%USERPROFILE%\\.ssh\\known_hosts"))
				{
					File.Delete("%USERPROFILE%\\.ssh\\known_hosts");
				}
				Thread.Sleep(5000);
			}
			catch
			{
				Process[] processesByName2 = Process.GetProcessesByName("iproxy");
				foreach (Process process2 in processesByName2)
				{
					((Thread)(object)process2).Start();
				}
				StartIproxy();
				if (File.Exists("%USERPROFILE%\\.ssh\\known_hosts"))
				{
					File.Delete("%USERPROFILE%\\.ssh\\known_hosts");
				}
			}
		}

		public void ldrestart()
		{
			SshClient sshClient = new SshClient("127.0.0.1", "root", "alpine");
			try
			{
				sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(5.0);
				((Thread)(object)sshClient).Start();
				((TextReader)(object)sshClient.CreateCommand("ldrestart")).ReadToEnd();
				Thread.Sleep(5000);
				((Thread)(object)sshClient).Start();
				Process[] processesByName = Process.GetProcessesByName("iproxy");
				foreach (Process process in processesByName)
				{
					((Thread)(object)process).Start();
				}
				StartIproxy();
				if (File.Exists("%USERPROFILE%\\.ssh\\known_hosts"))
				{
					File.Delete("%USERPROFILE%\\.ssh\\known_hosts");
				}
				Thread.Sleep(5000);
			}
			catch
			{
				Process[] processesByName2 = Process.GetProcessesByName("iproxy");
				foreach (Process process2 in processesByName2)
				{
					((Thread)(object)process2).Start();
				}
				StartIproxy();
				if (File.Exists("%USERPROFILE%\\.ssh\\known_hosts"))
				{
					File.Delete("%USERPROFILE%\\.ssh\\known_hosts");
				}
			}
		}

		static sshidevice()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
			sshclient = null;
		}
	}
}
