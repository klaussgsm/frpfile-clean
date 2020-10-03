using System;
using System.IO;
using System.Windows.Forms;

namespace iFrpfile_all_in_one_tool
{
	public class helloscreen
	{
		private sshidevice ohM_003D = new sshidevice();

		private libidevice oRM_003D = new libidevice();

		public static string path_Data;

		public static string path_Data_Temp;

		public static string path_Data_Backup;

		public string ERROR = "";

		private bool _002FRM_003D()
		{
			bool result = true;
			try
			{
				ohM_003D.SSH_Command("mount -o rw,union,update /");
				if (ohM_003D.get_IOS().Contains("12"))
				{
					ohM_003D.SPC_UploadFile(path_Data + "\\i12update", "/System/Library/PrivateFrameworks/PreferencesUI.framework/General.plist");
					ohM_003D.SPC_UploadFile(path_Data + "\\i12erase", "/System/Library/PrivateFrameworks/PreferencesUI.framework/Reset.plist");
					ohM_003D.SSH_Command("chmod 755 /System/Library/PrivateFrameworks/PreferencesUI.framework/General.plist");
					ohM_003D.SSH_Command("chmod 755 /System/Library/PrivateFrameworks/PreferencesUI.framework/Reset.plist");
					ohM_003D.SSH_Command("killall -9 backboardd");
				}
				else if (ohM_003D.get_IOS().Contains("13"))
				{
					ohM_003D.SPC_UploadFile(path_Data + "\\i13update", "/System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework/General.plist");
					ohM_003D.SPC_UploadFile(path_Data + "\\i13erase", "/System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework/Reset.plist");
					ohM_003D.SSH_Command("chmod 755 /System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework/General.plist");
					ohM_003D.SSH_Command("chmod 755 /System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework/Reset.plist");
					ohM_003D.SSH_Command("killall -9 backboardd");
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public bool TetheredBypass()
		{
			bool result = true;
			try
			{
				if (ohM_003D.get_IOS().Contains("12.0") || ohM_003D.get_IOS().Contains("12.1") || ohM_003D.get_IOS().Contains("12.2") || ohM_003D.get_IOS().Contains("12.3") || ohM_003D.get_IOS().Contains("12.4.1") || ohM_003D.get_IOS().Contains("12.4.2") || ohM_003D.get_IOS().Contains("12.4.3") || ohM_003D.get_IOS().Contains("12.4.4") || ohM_003D.get_IOS().Contains("13.0") || ohM_003D.get_IOS().Contains("13.1") || ohM_003D.get_IOS().Contains("13.2"))
				{
					Bypass12();
				}
				if (ohM_003D.get_IOS().Contains("12.4.5") || ohM_003D.get_IOS().Contains("12.4.6") || ohM_003D.get_IOS().Contains("12.4.7") || ohM_003D.get_IOS().Contains("12.4.8") || ohM_003D.get_IOS().Contains("12.4.9"))
				{
					Bypass1247();
				}
				if (ohM_003D.get_IOS().Contains("13.3") || ohM_003D.get_IOS().Contains("13.4") || ohM_003D.get_IOS().Contains("13.5") || ohM_003D.get_IOS().Contains("13.6") || ohM_003D.get_IOS().Contains("13.7") || ohM_003D.get_IOS().Contains("13.8") || ohM_003D.get_IOS().Contains("13.9"))
				{
					Bypass13();
				}
			}
			catch (Exception ex)
			{
				result = false;
				ERROR = ((TextReader)(object)ex).ReadToEnd();
			}
			return result;
		}

		public void Bypass12()
		{
			ohM_003D.SSH_Command("mount -o rw,union,update /");
			ohM_003D.SSH_Command("echo \" >> /.mount_rw");
			ohM_003D.SSH_Command("mv /Applications/Setup.app /Applications/Setup.app.crae");
			ohM_003D.SSH_Command("uicache --all");
			ohM_003D.SSH_Command("killall backboardd");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\pu.dll", "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
		}

		public void Bypass1247()
		{
			ohM_003D.SSH_Command("mount -o rw,union,update /");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\getfb.dll", "/var/mobile/Media/bypass");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\dmm1247.dll", "/etc/mobileact");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\lib.dll", "/System/Library/LaunchDaemons/com.iro.mobileact.plist");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\lib.dll", "/Library/LaunchDaemons/com.iro.mobileact.plist");
			ohM_003D.SSH_Command("cp /var/mobile/Media/bypass /usr/libexec/");
			ohM_003D.SSH_Command("mv /usr/libexec/mobileactivationd /usr/libexec/mobileactivationdbak");
			ohM_003D.SSH_Command("mv /usr/libexec/bypass /usr/libexec/mobileactivationd");
			ohM_003D.SSH_Command("chmod 755 /usr/libexec/mobileactivationd");
			ohM_003D.SSH_Command("chmod 755 /etc/mobileact");
			ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
			ohM_003D.SSH_Command("launchctl load /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
		}

		public void Bypass13()
		{
			ohM_003D.SSH_Command("mount -o rw,union,update /");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\getfb1.dll", "/var/mobile/Media/bypass1");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\getfb2.dll", "/var/mobile/Media/bypass2");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\dmm.dll", "/etc/mobileact");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\lib.dll", "/System/Library/LaunchDaemons/com.iro.mobileact.plist");
			ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\lib.dll", "/Library/LaunchDaemons/com.iro.mobileact.plist");
			ohM_003D.SSH_Command("cp /var/mobile/Media/bypass1 /usr/libexec/");
			ohM_003D.SSH_Command("mv /usr/libexec/mobileactivationd /usr/libexec/mobileactivationdbak");
			ohM_003D.SSH_Command("mv /usr/libexec/bypass1 /usr/libexec/mobileactivationd");
			ohM_003D.SSH_Command("chmod 0755 /usr/libexec/mobileactivationd");
			ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
			ohM_003D.SSH_Command("launchctl load /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
			ohM_003D.SSH_Command("cp /var/mobile/Media/bypass2 /usr/libexec/");
			ohM_003D.SSH_Command("mv /usr/libexec/mobileactivationd /usr/libexec/mobileactivationdbak");
			ohM_003D.SSH_Command("mv /usr/libexec/bypass2 /usr/libexec/mobileactivationd");
			ohM_003D.SSH_Command("chmod 0755 /usr/libexec/mobileactivationd");
			ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
			ohM_003D.SSH_Command("launchctl load /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist");
		}

		static helloscreen()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
			path_Data = Application.StartupPath + "\\Resources\\Data\\";
			path_Data_Temp = Application.StartupPath + "\\Resources\\Data\\Temp\\";
			path_Data_Backup = Application.StartupPath + "\\Resources\\Data\\Backup\\";
		}
	}
}
