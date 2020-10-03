using iFrpfile_all_in_one_tool;
using System;
using System.Windows.Forms;

namespace DBQ_003D
{
	internal static class CxQ_003D
	{
		[STAThread]
		private static void DRQ_003D()
		{
			Application.EnableVisualStyles();
			Control.CheckForIllegalCrossThreadCalls = false;
			Application.Run(new frmMain());
		}

		static CxQ_003D()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
		}
	}
}
