using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;

namespace iFrpfile_all_in_one_tool
{
	public class passcode
	{
		private sshidevice ohM_003D = new sshidevice();

		private libidevice oRM_003D = new libidevice();

		public string ex = "";

		public static string path_Data;

		public static string path_Data_Temp;

		public static string path_Data_Backup;

		public string Diagonal = "\\";

		public string iDiagonal = "/";

		private bool ChQ_003D = false;

		public static string path_file_activation_record;

		public static string path_file_internal_data_ark;

		public static string path_file_com_apple_purplebuddy;

		public static string path_file_com_apple_commcenter_device_specific_nobackup;

		public static string path_folder_GUID;

		public static string path_folder_internal_data_ark;

		public static string path_folder_activation_record;

		public static string path_folder_mobile_Library_Preferences;

		public static string path_folder_wireless_Library_Preferences;

		public static string iTemp;

		public void find_Path()
		{
			path_folder_GUID = ohM_003D.SSH_Command("find /private/var/containers/Data/System/ -iname 'internal'").Replace("Library/internal", "").Replace("\n", "")
				.Replace("//", "/");
			path_folder_internal_data_ark = path_folder_GUID + "Library/internal/";
			path_folder_activation_record = path_folder_GUID + "Library/activation_records/";
			path_folder_mobile_Library_Preferences = "/var/mobile/Library/Preferences/";
			path_folder_wireless_Library_Preferences = "/var/wireless/Library/Preferences/";
			path_file_internal_data_ark = path_folder_internal_data_ark + "data_ark.plist";
			path_file_activation_record = path_folder_activation_record + "activation_record.plist";
			path_file_com_apple_purplebuddy = path_folder_mobile_Library_Preferences + "com.apple.purplebuddy.plist";
			path_file_com_apple_commcenter_device_specific_nobackup = path_folder_wireless_Library_Preferences + "com.apple.commcenter.device_specific_nobackup.plist";
		}

		public bool Backup()
		{
			bool result = true;
			find_Path();
			if (File.Exists(path_Data_Temp))
			{
				Directory.Delete(path_Data_Temp, recursive: true);
			}
			if (!Directory.Exists(path_Data_Backup))
			{
				Directory.CreateDirectory(path_Data_Backup);
			}
			string text = ohM_003D.SSH_GetSerialNumber();
			string path = path_Data_Temp + text + Diagonal + "var" + Diagonal + "mobile" + Diagonal + "Library" + Diagonal + "FairPlay" + Diagonal;
			string path2 = path_Data_Temp + text + Diagonal + "var" + Diagonal + "wireless" + Diagonal + "Library" + Diagonal + "Preferences" + Diagonal;
			string path3 = path_Data_Temp + text + Diagonal + "var" + Diagonal + "containers" + Diagonal + "Data" + Diagonal + "System" + Diagonal + "X" + Diagonal + "Library" + Diagonal + "activation_records" + Diagonal;
			string path4 = path_Data_Temp + text + Diagonal + "var" + Diagonal + "containers" + Diagonal + "Data" + Diagonal + "System" + Diagonal + "X" + Diagonal + "Library" + Diagonal + "internal" + Diagonal;
			if (File.Exists(path_Data_Temp))
			{
				Directory.Delete(path_Data_Temp, recursive: true);
			}
			Directory.CreateDirectory(path_Data_Temp);
			Directory.CreateDirectory(path_Data_Temp + text + Diagonal + "var");
			Directory.CreateDirectory(path);
			Directory.CreateDirectory(path2);
			Directory.CreateDirectory(path3);
			Directory.CreateDirectory(path4);
			try
			{
				ohM_003D.SPC_DownloadFolder("/var/mobile/Library/FairPlay/", path);
				ohM_003D.SPC_DownloadFile(path_file_com_apple_commcenter_device_specific_nobackup, path2);
				ohM_003D.SPC_DownloadFile(path_file_activation_record, path3);
				ohM_003D.SPC_DownloadFile(path_file_internal_data_ark, path4);
			}
			catch (Exception ex)
			{
				this.ex = ((TextReader)(object)ex).ReadToEnd();
				result = false;
			}
			if (File.Exists(path_Data_Backup + text + ".zip"))
			{
				File.Delete(path_Data_Backup + text + ".zip");
			}
			ZipFile.CreateFromDirectory(path_Data_Temp + text + Diagonal, path_Data_Backup + text + ".zip");
			Thread.Sleep(5000);
			if (File.Exists(path_Data_Temp))
			{
				Directory.Delete(path_Data_Temp, recursive: true);
			}
			return result;
		}

		public bool Activate()
		{
			bool result = true;
			try
			{
				string text = ohM_003D.SSH_GetSerialNumber();
				string text2 = ohM_003D.SSH_Command("find /private/var/containers/Data/System/ -iname 'internal'").Replace("Library/internal", "").Replace("\n", "")
					.Replace("//", "/")
					.Replace("/private/var/containers/Data/System/", string.Empty)
					.Replace("/", string.Empty);
				if (!File.Exists(path_Data_Temp))
				{
					Directory.CreateDirectory(path_Data_Temp);
				}
				else
				{
					Directory.Delete(path_Data_Temp, recursive: true);
				}
				ZipFile.ExtractToDirectory(path_Data_Backup + text + ".zip", path_Data_Temp + text);
				oRM_003D.check_Pair();
				ohM_003D.SSH_Command("mount -o rw,union,update /");
				ohM_003D.SSH_Command("rm -rf /var/mobile/Media/" + text);
				ohM_003D.SSH_Command("mkdir /var/mobile/Media/Downloads/" + text);
				ohM_003D.SPC_UploadFolder(path_Data_Temp + text, "/var/mobile/Media/Downloads/" + text);
				ohM_003D.SSH_Command("mv -f /var/mobile/Media/Downloads/" + text + " /var/mobile/Media");
				ohM_003D.SSH_Command("chown -R mobile:mobile /var/mobile/Media/" + text);
				ohM_003D.SSH_Command("chmod -R 755 /var/mobile/Media/" + text);
				ohM_003D.SSH_Command("chmod 644 /var/mobile/Media/" + text + "/var/containers/Data/System/X/Library/internal/data_ark.plist");
				ohM_003D.SSH_Command("chmod 644 /var/mobile/Media/" + text + "/var/containers/Data/System/X/Library/internal/activation_record.plist");
				ohM_003D.SSH_Command("chmod 644 /var/mobile/Media/" + text + "/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
				ohM_003D.SSH_Command("killall backboardd && sleep 12");
				ohM_003D.SSH_Command("mv -f /var/mobile/Media/" + text + "/var/mobile/Library/FairPlay /var/mobile/Library/FairPlay");
				ohM_003D.SSH_Command("chmod 755 /var/mobile/Library/FairPlay");
				ohM_003D.SSH_Command("chflags nouchg /var/containers/Data/System/" + text2 + "/Library/internal/data_ark.plist");
				ohM_003D.SSH_Command("mv -f /var/mobile/Media/" + text + "/var/containers/Data/System/X/Library/internal/data_ark.plist /var/containers/Data/System/" + text2 + "/Library/internal/data_ark.plist");
				ohM_003D.SSH_Command("chmod 755 /var/containers/Data/System/" + text2 + "/Library/internal/data_ark.plist");
				ohM_003D.SSH_Command("chflags uchg /var/containers/Data/System/" + text2 + "/Library/internal/data_ark.plist");
				ohM_003D.SSH_Command("mkdir -p /var/containers/Data/System/" + text2 + "/Library/activation_records");
				ohM_003D.SSH_Command("chflags nouchg /var/containers/Data/System/" + text2 + "/Library/activation_records");
				ohM_003D.SSH_Command("mv -f /var/mobile/Media/" + text + "/var/containers/Data/System/X/Library/activation_records/activation_record.plist /var/containers/Data/System/" + text2 + "/Library/activation_records/");
				ohM_003D.SSH_Command("chmod 755 /var/containers/Data/System/" + text2 + "/Library/activation_records/activation_record.plist");
				ohM_003D.SSH_Command("chflags uchg /var/containers/Data/System/" + text2 + "/Library/activation_records/activation_record.plist");
				ohM_003D.SSH_Command("chflags nouchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
				ohM_003D.SSH_Command("mv -f /var/mobile/Media/" + text + "/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
				ohM_003D.SSH_Command("chown root:mobile /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
				ohM_003D.SSH_Command("chown 755 /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
				ohM_003D.SSH_Command("chmod 755 /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
				ohM_003D.SSH_Command("chflags uchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
				ohM_003D.SSH_Command("rm -rf /var/mobile/Media/" + text);
				ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
				ohM_003D.SSH_Command("launchctl load /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
				ohM_003D.SSH_Command("killall -9 backboardd");
				if (File.Exists(path_Data_Temp))
				{
					Directory.Delete(path_Data_Temp, recursive: true);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		static passcode()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
			path_Data = Application.StartupPath + "\\Resources\\Data\\";
			path_Data_Temp = Application.StartupPath + "\\Resources\\Data\\Temp\\";
			path_Data_Backup = Application.StartupPath + "\\Resources\\Data\\Backup\\";
			path_file_activation_record = "";
			path_file_internal_data_ark = "";
			path_file_com_apple_purplebuddy = "";
			path_file_com_apple_commcenter_device_specific_nobackup = "";
			path_folder_GUID = "";
			path_folder_internal_data_ark = "";
			path_folder_activation_record = "";
			path_folder_mobile_Library_Preferences = "";
			path_folder_wireless_Library_Preferences = "";
			iTemp = "/private/var/mobile/Media/";
		}
	}
}
