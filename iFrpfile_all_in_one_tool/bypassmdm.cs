using System;
using System.IO;
using System.Windows.Forms;

namespace iFrpfile_all_in_one_tool
{
	public class bypassmdm
	{
		private sshidevice ohM_003D = new sshidevice();

		public string ex = "";

		public bool BypassMDM()
		{
			bool result = false;
			try
			{
				ohM_003D.SSH_Command("rm -rf /var/containers/Shared/SystemGroup/systemgroup.com.apple.configurationprofiles/Library/ConfigurationProfiles/*");
				ohM_003D.SSH_Command("killall backboardd && sleep 7");
				ohM_003D.SPC_UploadFile(Application.StartupPath + "\\Resources\\Data\\mdm", "/var/containers/Shared/SystemGroup/systemgroup.com.apple.configurationprofiles/Library/ConfigurationProfiles/CloudConfigurationDetails.plist");
				ohM_003D.SSH_Command("chflags uchg /var/containers/Shared/SystemGroup/systemgroup.com.apple.configurationprofiles/Library/ConfigurationProfiles/CloudConfigurationDetails.plist");
				ohM_003D.SSH_Command("killall backboardd");
				result = true;
			}
			catch (Exception ex)
			{
				this.ex = ((TextReader)(object)ex).ReadToEnd();
			}
			return result;
		}

		static bypassmdm()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
		}
	}
}
