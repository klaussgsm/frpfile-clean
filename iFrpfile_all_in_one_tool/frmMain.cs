using MetroFramework.Controls;
using MetroFramework.Forms;
using Renci.SshNet;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace iFrpfile_all_in_one_tool
{
	public class frmMain : MetroForm
	{
		private libidevice oRM_003D;

		private sshidevice ohM_003D;

		private bypassmdm oxM_003D;

		private passcode pBM_003D;

		private hellobypass pRM_003D;

		private helloscreen phM_003D;

		public static SshClient sshclient;

		public string ERROR;

		public string ApplivationPath;

		public static string path_Data;

		public static string path_Data_Temp;

		public static string path_Data_Backup;

		public static string key;

		public static string SerialNumber;

		public string carrie;

		private bool pxM_003D;

		private IContainer qBM_003D;

		private MetroPanel qRM_003D;

		private MetroPanel qhM_003D;

		private MetroPanel qxM_003D;

		private MetroPanel rBM_003D;

		private MetroPanel rRM_003D;

		private MetroTabControl rhM_003D;

		private MetroTabPage rxM_003D;

		private MetroButton sBM_003D;

		private MetroButton sRM_003D;

		private MetroTabPage shM_003D;

		private MetroLink sxM_003D;

		private MetroButton tBM_003D;

		private MetroTabPage tRM_003D;

		private MetroButton thM_003D;

		private Label txM_003D;

		private MetroTabPage uBM_003D;

		private MetroButton uRM_003D;

		private MetroButton uhM_003D;

		private MetroPanel uxM_003D;

		private GroupBox vBM_003D;

		private MetroLabel vRM_003D;

		private MetroLabel vhM_003D;

		private TableLayoutPanel vxM_003D;

		private MetroLabel wBM_003D;

		private MetroLabel wRM_003D;

		private MetroLabel whM_003D;

		private MetroLabel wxM_003D;

		private MetroLabel xBM_003D;

		private MetroLabel xRM_003D;

		private MetroLabel xhM_003D;

		private MetroLabel xxM_003D;

		private MetroLabel yBM_003D;

		private MetroLabel yRM_003D;

		private MetroLabel yhM_003D;

		private MetroLabel yxM_003D;

		private MetroLabel zBM_003D;

		private MetroButton zRM_003D;

		private MetroLabel zhM_003D;

		private MetroTabControl zxM_003D;

		private MetroTabPage _0BM_003D;

		private MetroTabPage _0RM_003D;

		private MetroButton _0hM_003D;

		private MetroButton _0xM_003D;

		private MetroLink _1BM_003D;

		private MetroLink _1RM_003D;

		private MetroLabel _1hM_003D;

		private MetroLabel _1xM_003D;

		private Timer _2BM_003D;

		private Timer _2RM_003D;

		private MetroTabControl _2hM_003D;

		private MetroTabPage _2xM_003D;

		private MetroLabel _3BM_003D;

		public MetroProgressSpinner metroProgressSpinner1;

		private MetroTabPage _3RM_003D;

		private MetroButton _3hM_003D;

		private MetroTextBox _3xM_003D;

		private MetroLabel _4BM_003D;

		private MetroButton _4RM_003D;

		private MetroLink _4hM_003D;

		private MetroLabel _4xM_003D;

		private MetroLabel _5BM_003D;

		private MetroLink _5RM_003D;

		private MetroTextBox _5hM_003D;

		private MetroLabel _5xM_003D;

		private void _6BM_003D(object sender, EventArgs e)
		{
		}

		public void checkversion()
		{
		}

		public bool checkPhoneConnected()
		{
			return false;
		}

		public void checkSSH()
		{
		}

		private void _6RM_003D(object sender, EventArgs e)
		{
		}

		public void info_iDevice()
		{
		}

		public void clear_Text()
		{
		}

		public void btnEnabled(bool x)
		{
		}

		public void btn2Enabled(bool x)
		{
		}

		public bool IsConnectedToInternet()
		{
			return false;
		}

		private void _6hM_003D(object sender, EventArgs e)
		{
		}

		private void _6xM_003D(object sender, EventArgs e)
		{
		}

		private void _7BM_003D(object sender, FormClosingEventArgs e)
		{
		}

		private void _7RM_003D(object sender, EventArgs e)
		{
		}

		private void _7hM_003D()
		{
		}

		public void BackupPassCode()
		{
		}

		public void Erase_all()
		{
		}

		public void ActivatePassocde()
		{
		}

		private void _7xM_003D(object sender, EventArgs e)
		{
		}

		private void _8BM_003D(object sender, EventArgs e)
		{
		}

		private void _8RM_003D(object sender, EventArgs e)
		{
		}

		public void UntetheredBypass()
		{
		}

		private void _8hM_003D(object sender, EventArgs e)
		{
		}

		private void _8xM_003D(object sender, EventArgs e)
		{
		}

		public void TetheredBypass()
		{
		}

		private void _9BM_003D(object sender, EventArgs e)
		{
		}

		private void _9RM_003D(object sender, EventArgs e)
		{
		}

		private void _9hM_003D(object sender, EventArgs e)
		{
		}

		private void _9xM_003D(object sender, EventArgs e)
		{
		}

		private void _002FBM_003D(object sender, EventArgs e)
		{
		}

		public void doblockUpdate_Reset()
		{
		}

		private bool _002FRM_003D()
		{
			return false;
		}

		private void _002FhM_003D(object sender, EventArgs e)
		{
		}

		private void _002FxM_003D(object sender, EventArgs e)
		{
		}

		private void ABQ_003D(object sender, EventArgs e)
		{
		}

		private void ARQ_003D(object sender, EventArgs e)
		{
		}

		private void AhQ_003D(object sender, EventArgs e)
		{
		}

		private void AxQ_003D(object sender, EventArgs e)
		{
		}

		private void BBQ_003D()
		{
		}

		private void BRQ_003D(object sender, EventArgs e)
		{
		}

		public bool check404(string url)
		{
			return false;
		}

		protected override void Dispose(bool disposing)
		{
		}

		private void BhQ_003D()
		{
		}

		static frmMain()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
			sshclient = null;
			path_Data = Application.StartupPath + "\\Resources\\Data\\";
			path_Data_Temp = Application.StartupPath + "\\Resources\\Data\\Temp\\";
			path_Data_Backup = Application.StartupPath + "\\Resources\\Data\\Backup\\";
			key = "frpfile.com";
			SerialNumber = "";
		}
	}
}
