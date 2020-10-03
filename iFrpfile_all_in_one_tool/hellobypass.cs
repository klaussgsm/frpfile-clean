using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace iFrpfile_all_in_one_tool
{
	public class hellobypass
	{
		private sshidevice ohM_003D = new sshidevice();

		private libidevice oRM_003D = new libidevice();

		public static string path_Data;

		public static string path_Data_Temp;

		public static string path_Data_Backup;

		public string ERROR = "";

		public string server = "";

		public static string key;

		private void BxQ_003D(string carrie)
		{
			string contents = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\"><plist version=\"1.0\"><dict><key>BAD_SIM</key><string>" + carrie + "</string><key>LOCKED_SIM</key><string>" + carrie + "</string><key>NO_SERVICE</key><string>" + carrie + "</string><key>NO_SIM</key><string>" + carrie + "</string><key>SEARCHING</key><string>" + carrie + "</string><key>SOS_ONLY</key><string>" + carrie + "</string></dict></plist>";
			if (!File.Exists(path_Data + "\\SystemStatusServer.strings"))
			{
				File.Create(path_Data + "\\ystemStatusServer.strings");
			}
			else
			{
				File.WriteAllText(path_Data + "\\SystemStatusServer.strings", contents);
			}
		}

		public void changeStatus(string carrie)
		{
			BxQ_003D(carrie);
			List<string> list = null;
			string text = ohM_003D.SSH_Command("ls /System/Library/PrivateFrameworks/SystemStatusServer.framework");
			list = text.Split('\n').ToList();
			foreach (string item in list)
			{
				if (item.Contains("lproj"))
				{
					ohM_003D.SPC_UploadFile(path_Data + "\\SystemStatusServer.strings", "/System/Library/PrivateFrameworks/SystemStatusServer.framework/" + item + "/SystemStatusServer.strings");
				}
			}
			ohM_003D.SSH_Command("chmod 755 /System/Library/PrivateFrameworks/SystemStatusServer.framework");
			ohM_003D.SSH_Command("killall -9 backboardd");
		}

		public string read_Server()
		{
			string result = "";
			if (File.Exists(path_Data + "config.txt"))
			{
				FileStream fileStream = new FileStream(path_Data + "config.txt", FileMode.Open);
				StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8);
				result = ((TextReader)streamReader).ReadToEnd();
				fileStream.Close();
			}
			else
			{
				MessageBox.Show("You must get new string server. Open site get server now", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
			}
			return result;
		}

		public string Decrypt(string toDecrypt)
		{
			string result = "";
			try
			{
				bool flag = true;
				byte[] array = Convert.FromBase64String(toDecrypt);
				byte[] array2;
				if (flag)
				{
					MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
					array2 = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(key));
				}
				else
				{
					array2 = Encoding.UTF8.GetBytes(key);
				}
				TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
				((SymmetricAlgorithm)tripleDESCryptoServiceProvider).Key = array2;
				tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
				tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
				ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
				byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
				result = Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				MessageBox.Show("Wrong string server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return result;
		}

		public bool untetheredBypass(string frpfile)
		{
			bool result = true;
			try
			{
				Bypass(frpfile);
			}
			catch (Exception ex)
			{
				ERROR = ((TextReader)(object)ex).ReadToEnd();
				result = false;
			}
			return result;
		}

		public void Bypass(string frpfile)
		{
			while (!oRM_003D.check_Pair())
			{
				MessageBox.Show("Unlock Phone and press Trust", "Device not paired", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			ohM_003D.StartIproxy();
			int num = 0;
			while (true)
			{
				ohM_003D.SSH_Command("mount -o rw,union,update /");
				ohM_003D.SPC_UploadFile(path_Data + "\\RAPEM", "/System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem");
				ohM_003D.SSH_Command("snappy -f / -r `snappy -f / -l | sed -n 2p` -t orig-fs");
				ohM_003D.SPC_UploadFile(path_Data + "\\boot.tar.lzma", "/tmp/boot.tar.lzma");
				ohM_003D.SPC_UploadFile(path_Data + "\\lzma", "/sbin/lzma");
				ohM_003D.SSH_Command("chmod 755 /sbin/lzma");
				ohM_003D.SSH_Command("lzma -d -v /tmp/boot.tar.lzma");
				ohM_003D.SSH_Command("tar -C / -xvf /tmp/boot.tar && rm -r /tmp/boot.tar");
				ohM_003D.SSH_Command("chmod 755 /usr/libexec/substrate && /usr/libexec/substrate");
				ohM_003D.SSH_Command("chmod 755 /usr/libexec/substrated && /usr/libexec/substrated");
				ohM_003D.SSH_Command("killall -9 backboardd");
				ohM_003D.SSH_Command("killall CommCenter && killall CommCenterMobileHelper");
				ohM_003D.SPC_UploadFile(path_Data + "\\ulib", "/Library/MobileSubstrate/DynamicLibraries/untethered.dylib");
				ohM_003D.SPC_UploadFile(path_Data + "\\upl", "/Library/MobileSubstrate/DynamicLibraries/untethered.plist");
				ohM_003D.SSH_Command("chmod +x /Library/MobileSubstrate/DynamicLibraries/untethered.dylib");
				ohM_003D.SSH_Command("killall -9 SpringBoard mobileactivationd");
				oRM_003D.iDevice_Deactivate();
				oRM_003D.iDevice_Activate(Decrypt(read_Server()));
				ohM_003D.SPC_UploadFile(path_Data + "\\iulib", "/Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
				ohM_003D.SPC_UploadFile(path_Data + "\\iupl", "/Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
				ohM_003D.SSH_Command("chmod +x /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
				ohM_003D.SSH_Command("killall -9 SpringBoard mobileactivationd");
				oRM_003D.iDevice_Activate(Decrypt(read_Server()));
				ohM_003D.SSH_Command("rm /Library/MobileSubstrate/DynamicLibraries/untethered.dylib && rm /Library/MobileSubstrate/DynamicLibraries/untethered.plist");
				string a = ohM_003D.SSH_Command("ls -a /var/containers/Data/System/*/Library/activation_records/activation_record.plist");
				if (!(a != ""))
				{
					break;
				}
				num++;
				if (num == 5)
				{
					MessageBox.Show("Activate Failed. Pls try restore full and bypass again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			ohM_003D.SPC_UploadFile(path_Data + "\\ldrestart", "/usr/bin/ldrestart");
			ohM_003D.SSH_Command("chmod 755 /usr/bin/ldrestart");
			ohM_003D.SPC_UploadFile(path_Data + "\\plutil", "/usr/bin/plutil");
			ohM_003D.SSH_Command("chmod 755 /usr/bin/plutil");
			changeStatus(frpfile);
			_002FRM_003D();
			DisableUpdate_EraseAll();
			ohM_003D.SSH_Command("killall -9 backboardd");
			ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.CommCenterRootHelper.plist");
			ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.CommCenterMobileHelper.plist");
			ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.CommCenter.plist");
			ohM_003D.SSH_Command("chflags nouchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("plutil -backup /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("rm -rf /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("plutil -create /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("plutil -key kPostponementTicket -remove /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("plutil -key kPostponementTicket -dict /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			string str = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4KPCFET0NUWVBFIHBsaXN0IFBVQkxJQyAiLS8vQXBwbGUvL0RURCBQTElTVCAxLjAvL0VOIiAiaHR0cDovL3d3dy5hcHBsZS5jb20vRFREcy9Qcm9wZXJ0eUxpc3QtMS4wLmR0ZCI+CjxwbGlzdCB2ZXJzaW9uPSIxLjAiPgo8ZGljdD4KCTxrZXk+QWN0aXZhdGlvblJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkFjdGl2YXRpb25SYW5kb21uZXNzPC9rZXk+CgkJPHN0cmluZz4zMGI2MGZkMC02Njc0LTQ3NzgtYmIxNC1mNGZhOTQ0MWQ0Yzg8L3N0cmluZz4KCQk8a2V5PkFjdGl2YXRpb25TdGF0ZTwva2V5PgoJCTxzdHJpbmc+VW5hY3RpdmF0ZWQ8L3N0cmluZz4KCQk8a2V5PkZNaVBBY2NvdW50RXhpc3RzPC9rZXk+CgkJPHRydWUvPgoJPC9kaWN0PgoJPGtleT5CYXNlYmFuZFJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkFjdGl2YXRpb25SZXF1aXJlc0FjdGl2YXRpb25UaWNrZXQ8L2tleT4KCQk8dHJ1ZS8+CgkJPGtleT5CYXNlYmFuZEFjdGl2YXRpb25UaWNrZXRWZXJzaW9uPC9rZXk+CgkJPHN0cmluZz5WMjwvc3RyaW5nPgoJCTxrZXk+QmFzZWJhbmRDaGlwSUQ8L2tleT4KCQk8aW50ZWdlcj4xMjM0NTY3PC9pbnRlZ2VyPgoJCTxrZXk+QmFzZWJhbmRNYXN0ZXJLZXlIYXNoPC9rZXk+CgkJPHN0cmluZz44Q0IxMDcwRDk1QjlDRUU0QzgwMDAwNUUyMTk5QkI4RkIxODNCMDI3MTNBNTJEQjVFNzVDQTJBNjE1NTM2MTgyPC9zdHJpbmc+CgkJPGtleT5CYXNlYmFuZFNlcmlhbE51bWJlcjwva2V5PgoJCTxkYXRhPgoJCUVnaGRDdz09CgkJPC9kYXRhPgoJCTxrZXk+SW50ZXJuYXRpb25hbE1vYmlsZUVxdWlwbWVudElkZW50aXR5PC9rZXk+CgkJPHN0cmluZz4xMjM0NTY3ODkxMjM0NTY8L3N0cmluZz4KCQk8a2V5Pk1vYmlsZUVxdWlwbWVudElkZW50aWZpZXI8L2tleT4KCQk8c3RyaW5nPjEyMzQ1Njc4OTEyMzQ1PC9zdHJpbmc+CgkJPGtleT5TSU1TdGF0dXM8L2tleT4KCQk8c3RyaW5nPmtDVFNJTVN1cHBvcnRTSU1TdGF0dXNOb3RJbnNlcnRlZDwvc3RyaW5nPgoJCTxrZXk+U3VwcG9ydHNQb3N0cG9uZW1lbnQ8L2tleT4KCQk8dHJ1ZS8+CgkJPGtleT5rQ1RQb3N0cG9uZW1lbnRJbmZvUFJMTmFtZTwva2V5PgoJCTxpbnRlZ2VyPjA8L2ludGVnZXI+CgkJPGtleT5rQ1RQb3N0cG9uZW1lbnRJbmZvU2VydmljZVByb3Zpc2lvbmluZ1N0YXRlPC9rZXk+CgkJPGZhbHNlLz4KCTwvZGljdD4KCTxrZXk+RGV2aWNlQ2VydFJlcXVlc3Q8L2tleT4KCTxkYXRhPgoJTFMwdExTMUNSVWRKVGlCRFJWSlVTVVpKUTBGVVJTQlNSVkZWUlZOVUxTMHRMUzBLVFVsSlFuaEVRME5CVXpCRFFWRkIKCWQyZFpUWGhNVkVGeVFtZE9Wa0pCVFZSS1JVa3pUbXRSTUU1RlJUVk1WVmt6VGpCUmRFNUZVVEJOYVRBMFVWVktRZzBLCglURlJyZUZKcVdrVlNSRWw1VWtWS1IwNXFSVXhOUVd0SFFURlZSVUpvVFVOV1ZrMTRRM3BCU2tKblRsWkNRV2RVUVd0TwoJYWpaeFNVbHRUbmxXU21WMU5sTTJVak40UVcxT1RXNWFjREpHTDNoRVNIRjViVmxVT1ZoT1JFdzBjRlJaYjFnMmF6QmsKCVFrMVNTWGRGUVZsRVZsRlJTQTBLUlhkc1JHUllRbXhqYmxKd1ltMDRlRVY2UVZKQ1owNVdRa0Z2VkVOclJuZGpSM2hzCglTVVZzZFZsNU5IaEVla0ZPUW1kT1ZrSkJjMVJDYld4UllVYzVkUTBLV2xSRFFtNTZRVTVDWjJ0eGFHdHBSemwzTUVKQgoJUVUxQk1FZERVM0ZIVTBsaU0wUlJSVUpDVVZWQlFUUkhRa0ZETDJ4eWJHVlJUamR3UVEwS00yaEhWVlkwU0ZsU1lXdHYKCWFrazRPV3d4YUZKdmRqQlROREJPTUhBeU1UaHJUV295YkRGT2EzUXdWWEJxV2s5RU5WVldlVGRDT0VsT1FrSm1RMmxNCglNZzBLWnk4dkx5dHpaVVZoVjFjMGFEWXdUM0pOZG5KbFFWQTBNR0psVTJaUFlucE1WR3hYUzJGV2NXRnJNV1JGVGpSSgoJTkd4TVRYaHBlVFVyYjNwSVpqWmlWdzBLVGl0bldFSlVNMjl4WkhWRFF6RldWelZKV25aMlpFUlNWRWx3YUZoNmEyRUsKCVVVVkdRVUZQUW1wUlFYZG5XV3REWjFsRlFYSlVhMVpFZDBGV01IbHRZazVWUm14ME0yeExjMHRCWkEwS2JuYzBTRlpPCglaMEZ1UkhoaWRRMEtRVUpXV1VSMlNGaEJNREZNV0ZOS1F5dHRkamd5VFZSSWQySk5ORVF2V2xJclJFaFpRV1kyWXlzNQoJYVc1TlJtUk9PR2xaV0hSSWFFOXdjV3MwYVd4TlR3MEtZMnRuWWtsNlMwb3lOWFJPWTFKVWMwOXdWVU5CZDBWQlFXRkIKCUxTMHRMUzFGVGtRZ1EwVlNWRWxHU1VOQlZFVWdVa1ZSVlVWVFZDMHRMUzB0Cgk8L2RhdGE+Cgk8a2V5PkRldmljZUlEPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PlNlcmlhbE51bWJlcjwva2V5PgoJCTxzdHJpbmc+RlIxUDJHSDhKOFhIPC9zdHJpbmc+CgkJPGtleT5VbmlxdWVEZXZpY2VJRDwva2V5PgoJCTxzdHJpbmc+ZDk4OTIwOTZjZjM0MTFlYTg3ZDAwMjQyYWMxMzAwMDNmMzQxMWU0Mjwvc3RyaW5nPgoJPC9kaWN0PgoJPGtleT5EZXZpY2VJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkJ1aWxkVmVyc2lvbjwva2V5PgoJCTxzdHJpbmc+MThGMDA8L3N0cmluZz4KCQk8a2V5PkRldmljZUNsYXNzPC9rZXk+CgkJPHN0cmluZz5pUGhvbmU8L3N0cmluZz4KCQk8a2V5PkRldmljZVZhcmlhbnQ8L2tleT4KCQk8c3RyaW5nPkI8L3N0cmluZz4KCQk8a2V5Pk1vZGVsTnVtYmVyPC9rZXk+CgkJPHN0cmluZz5NTExOMjwvc3RyaW5nPgoJCTxrZXk+T1NUeXBlPC9rZXk+CgkJPHN0cmluZz5pUGhvbmUgT1M8L3N0cmluZz4KCQk8a2V5PlByb2R1Y3RUeXBlPC9rZXk+CgkJPHN0cmluZz5pUGhvbmUwLDA8L3N0cmluZz4KCQk8a2V5PlByb2R1Y3RWZXJzaW9uPC9rZXk+CgkJPHN0cmluZz4xNC4wLjA8L3N0cmluZz4KCQk8a2V5PlJlZ2lvbkNvZGU8L2tleT4KCQk8c3RyaW5nPkxMPC9zdHJpbmc+CgkJPGtleT5SZWdpb25JbmZvPC9rZXk+CgkJPHN0cmluZz5MTC9BPC9zdHJpbmc+CgkJPGtleT5SZWd1bGF0b3J5TW9kZWxOdW1iZXI8L2tleT4KCQk8c3RyaW5nPkExMjM0PC9zdHJpbmc+CgkJPGtleT5TaWduaW5nRnVzZTwva2V5PgoJCTx0cnVlLz4KCQk8a2V5PlVuaXF1ZUNoaXBJRDwva2V5PgoJCTxpbnRlZ2VyPjEyMzQ1Njc4OTEyMzQ8L2ludGVnZXI+Cgk8L2RpY3Q+Cgk8a2V5PlJlZ3VsYXRvcnlJbWFnZXM8L2tleT4KCTxkaWN0PgoJCTxrZXk+RGV2aWNlVmFyaWFudDwva2V5PgoJCTxzdHJpbmc+Qjwvc3RyaW5nPgoJPC9kaWN0PgoJPGtleT5Tb2Z0d2FyZVVwZGF0ZVJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkVuYWJsZWQ8L2tleT4KCQk8dHJ1ZS8+Cgk8L2RpY3Q+Cgk8a2V5PlVJS0NlcnRpZmljYXRpb248L2tleT4KCTxkaWN0PgoJCTxrZXk+Qmx1ZXRvb3RoQWRkcmVzczwva2V5PgoJCTxzdHJpbmc+ZmY6ZmY6ZmY6ZmY6ZmY6ZmY8L3N0cmluZz4KCQk8a2V5PkJvYXJkSWQ8L2tleT4KCQk8aW50ZWdlcj4yPC9pbnRlZ2VyPgoJCTxrZXk+Q2hpcElEPC9rZXk+CgkJPGludGVnZXI+MzI3Njg8L2ludGVnZXI+CgkJPGtleT5FdGhlcm5ldE1hY0FkZHJlc3M8L2tleT4KCQk8c3RyaW5nPmZmOmZmOmZmOmZmOmZmOmZmPC9zdHJpbmc+CgkJPGtleT5VSUtDZXJ0aWZpY2F0aW9uPC9rZXk+CgkJPGRhdGE+CgkJTUlJRDB3SUJBakNDQTh3RUlQNEMzc3FRdFAxUzJod0JaekNvSGNzb0gyeE51NWMrYTRRNDVvSjFNS0YzCgkJQkVFRTJlOTNlb1ZPeHVmMGVLUFVxTkVnNnpNbEJzTnEranIrUnFNQXhTaFZBL2NUNW9ua3IwdCtFMEhLCgkJblNkdkhNMi9GZXRyT3FpT0k0RHZIUElEVzBEMnVBUVEzaW9iUHdhQWxGbFhIUFdyOE1KLyt3UVFHVGxuCgkJRVhPMTZOdDJrVUUrdy8vQmxHd1Q4V3hSZXkvSU41SW1NbGtZelpsSnpack83dVl0bHBlZ3k2K3hJaWwyCgkJQjJYbHk0aUd4UlppUld5NXNLcFFvMll6b0pFbW1XU25manUwY1UyL3JiOUZCdnVWaS9rV1NGbkFrdDR5CgkJcVF3NGswaWJ0cDVXK1lVQ0NvZm8zeWVuak0yVWMwbit5SExyU20wRTlPUDNwdExUN3ZHcnJma3IzWFJpCgkJdHdEcGRCT3NzK1h6SEFRWEt1cG85WGkxUW1ObGp1VGoxakpZbzZNc1kyOURYOUVacFdEdmpJc0l5THd4CgkJQjRjbUlTVWY4Qm5yUlFHOURBM01lYzZiaFRkUEJjdUtXZHBCbm5DMlY4V3BmTXBwVUQ2U2RndW5pejZ6CgkJTEcwNmNGR3dvUXZuWXhRa1Vra2pkWWR6NG85eXM5L3ZxQ2JxZnBuNHRjZEkyMWM5Z29Nd0xoRHNoYms1CgkJUENaQnNoNUY0U1JSaWdBV3JBU0NBejk4MkI3bzhwQ0NaL2pZK3laQ3pBb3J6SG5zR2Z2d0tpSlBBTWppCgkJZTA0RzRqSk04cEpRUU5uWmFhUCt0RmVsZGhER1FubzA0dmZKRFkzOEZGTSthZUN3elJyQy9DUGJrZVpRCgkJNXR5NTdMSXNzMUhyUmUzSTFjK0ZMNXBuZmwvaEsxQjF1QTRHRDRWbFkxU0xMMXk1ajRHdUZUM1hTeHpiCgkJWlIvZmJEa1V5VHNUM3I2eGdoWnRNNEJYSW9hNjJaREMzSVBtT2J4S2JobGFLQTRtSzJzM1FCNFZjNlMvCgkJbTZ1YTZQakwvQjE1QzBjTGpyMUNNb0x0Lzc4TFVRV21GRXV5SkhkdnRTNnhIbWtMRG9FZW1tMHlDcGJqCgkJMmhrRmt2d3dISlg2SDFiUm1KWS9HUmY1UXVIWDVKdlk3ZGhOY2YzNENmaVExRHdwZ2VKUkw5eTN2SG0vCgkJZkFSV0JxWDNkWjV1VUpXcUNzMklvMFdIRGdqMTh3cW5vUEw2QnRHcjVhWEJFeGF3WkpGT1ZOcVZjV2lPCgkJOE9LMzhuSDFKaGcxVk44UURBelhmTEpjQ2w0UEN6Mm5sVlpSMDl1WnF0NlpPaXFjVUNyZ3hZbTdIQktaCgkJOS9BRmIyVmxLUFRZTTNueXBDeGh5MmNMQnowK3RCK0V6V0hTbjlzU3FMelN1eFBOdGIxY21FMno5OFNoCgkJMk1UVzJaWk42NWdvYkxrSU5wbzdUb1RBMm50cHY1ZjBqdlhpVnZIV1V1dmhUSVlLZG4vKzA0czNJQ0VLCgkJQVlJQ0NPNjgvakxucDVQUERuRmVsQ3Z1d0dFRTFkb0lMNzZ6UllNOWlrWTJHRVB5NW5XdW1ydXp4U2RCCgkJMURBNnNOeUxQanN2QnBnYUVnWmI0OUpXSjlERU5vYWZKeGQ4dlBoRnpORHZEL0NRKzU4VGtCYmYwWEVLCgkJa2xIRzdzOFY0SkRsYS9jMTBjSDcyWS8wL0lOUi9kUVk1V3FSaHNiSEVFalBVekdDTGNVPQoJCTwvZGF0YT4KCQk8a2V5PldpZmlBZGRyZXNzPC9rZXk+CgkJPHN0cmluZz5mZjpmZjpmZjpmZjpmZjpmZjwvc3RyaW5nPgoJPC9kaWN0Pgo8L2RpY3Q+CjwvcGxpc3Q+";
			ohM_003D.SSH_Command("plutil -key kPostponementTicket -key ActivationTicket -string " + str + " /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("plutil -key kPostponementTicket -key ActivityURL -string https://albert.apple.com/deviceservices/activity /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("plutil -key kPostponementTicket -key PhoneNumberNotificationURL -string https://albert.apple.com/deviceservices/phoneHome /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("plutil -key kPostponementTicket -key ActivationState -string FactoryActivated /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("chflags uchg /var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist");
			ohM_003D.SSH_Command("launchctl load /System/Library/LaunchDaemons/com.apple.CommCenterRootHelper.plist");
			ohM_003D.SSH_Command("launchctl load /System/Library/LaunchDaemons/com.apple.CommCenterMobileHelper.plist");
			ohM_003D.SSH_Command("launchctl load /System/Library/LaunchDaemons/com.apple.CommCenter.plist");
			ohM_003D.ldrestart();
			ohM_003D.SSH_Command("chflags nouchg /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
			ohM_003D.SSH_Command("rm -rf /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
			ohM_003D.SPC_UploadFile(path_Data + "\\com.apple.purplebuddy.plist", "/var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
			ohM_003D.SSH_Command("chmod 600 /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
			ohM_003D.SSH_Command("uicache --all && chflags uchg /var/mobile/Library/Preferences/com.apple.purplebuddy.plist");
			ohM_003D.SSH_Command("killall -9 backboardd");
			ohM_003D.ldrestart();
			ohM_003D.SSH_Command("chmod +x /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib");
			ohM_003D.SSH_Command("killall -9 backboardd");
			ohM_003D.SSH_Command("rm /Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib && rm /Library/MobileSubstrate/DynamicLibraries/iuntethered.plist");
			ohM_003D.ldrestart();
			ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.CommCenterRootHelper.plist");
			ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.CommCenterMobileHelper.plist");
			ohM_003D.SSH_Command("launchctl unload /System/Library/LaunchDaemons/com.apple.CommCenter.plist");
		}

		public void DisableUpdate_EraseAll()
		{
			ohM_003D.SSH_Command("launchctl unload -w /System/Library/LaunchDaemons/com.apple.mobile.obliteration.plist");
			ohM_003D.SSH_Command("launchctl unload -w /System/Library/LaunchDaemons/com.apple.OTACrashCopier.plist");
			ohM_003D.SSH_Command("launchctl unload -w /System/Library/LaunchDaemons/com.apple.mobile.softwareupdated.plist");
			ohM_003D.SSH_Command("launchctl unload -w /System/Library/LaunchDaemons/com.apple.OTATaskingAgent.plist");
			ohM_003D.SSH_Command("launchctl unload -w /System/Library/LaunchDaemons/com.apple.OTATaskingAgent.plist");
			ohM_003D.SSH_Command("mount -o rw,union,update /");
			ohM_003D.SPC_UploadFile(path_Data + "\\bebeo", "/tmp/bebeo.tar");
			ohM_003D.SPC_UploadFile(path_Data + "\\lzma", "/sbin/lzma");
			ohM_003D.SSH_Command("chmod 755 /sbin/lzma");
			ohM_003D.SSH_Command("lzma -d -v /tmp/bebeo.tar");
			ohM_003D.SSH_Command("tar -C / -xvf /tmp/bebeo.tar && rm -r /tmp/bebeo.tar");
			ohM_003D.SSH_Command("chmod 755 /usr/libexec/substrate && /usr/libexec/substrate");
			ohM_003D.SSH_Command("chmod 755 /usr/libexec/substrated && /usr/libexec/substrated");
			ohM_003D.SSH_Command("killall -9 backboardd");
			ohM_003D.SSH_Command("chmod +x Library/MobileSubstrate/DynamicLibraries/MobileSafety.dylib");
			ohM_003D.SSH_Command("chmod +x Library/MobileSubstrate/DynamicLibraries/OTADisabler.dylib");
		}

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
				else if (ohM_003D.get_IOS().Contains("14"))
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

		static hellobypass()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
			path_Data = Application.StartupPath + "\\Resources\\Data\\";
			path_Data_Temp = Application.StartupPath + "\\Resources\\Data\\Temp\\";
			path_Data_Backup = Application.StartupPath + "\\Resources\\Data\\Backup\\";
			key = "frpfile.com";
		}
	}
}
