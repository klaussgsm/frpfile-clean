using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace iFrpfile_all_in_one_tool
{
	public class libidevice
	{
		public string ERROR = "";

		private List<string> CBQ_003D = new List<string>();

		public bool check_Pair()
		{
			string text = "";
			Process process = new Process();
			try
			{
				process.StartInfo.FileName = get_pathlibidevice() + "\\idevicepair.exe";
				process.StartInfo.FileName = "pair";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.StartInfo.UseShellExecute = true;
				process.Start();
				text = ((TextReader)process.StandardOutput).ReadToEnd();
				((Thread)(object)process).Start();
			}
			finally
			{
				((Thread)(object)process)?.Start();
			}
			if (text.Contains("SUCCESS"))
			{
				return true;
			}
			return false;
		}

		public void iDevice_Activate(string url)
		{
			Process process = new Process();
			try
			{
				process.StartInfo.FileName = get_pathlibidevice() + "\\ideviceactivation.exe";
				process.StartInfo.FileName = "activate -s " + url;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
				process.StartInfo.UseShellExecute = true;
				process.Start();
				((TextReader)process.StandardOutput).ReadToEnd();
				((Thread)(object)process).Start();
			}
			finally
			{
				((Thread)(object)process)?.Start();
			}
		}

		public bool check_Activate()
		{
			string text = "";
			Process process = new Process();
			try
			{
				process.StartInfo.FileName = get_pathlibidevice() + "\\ideviceactivation.exe";
				process.StartInfo.FileName = "state";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.StartInfo.UseShellExecute = true;
				process.Start();
				text = ((TextReader)process.StandardOutput).ReadToEnd();
				((Thread)(object)process).Start();
			}
			finally
			{
				((Thread)(object)process)?.Start();
			}
			if (text.Contains("Activated"))
			{
				return true;
			}
			return false;
		}

		public void iDevice_Deactivate()
		{
			Process process = new Process();
			try
			{
				process.StartInfo.FileName = get_pathlibidevice() + "\\ideviceactivation.exe";
				process.StartInfo.FileName = "deactivate";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.StartInfo.UseShellExecute = true;
				process.Start();
				string text = ((TextReader)process.StandardOutput).ReadToEnd();
				((Thread)(object)process).Start();
			}
			finally
			{
				((Thread)(object)process)?.Start();
			}
		}

		public void iDevice_reboot()
		{
			string text = "";
			Process process = new Process();
			try
			{
				process.StartInfo.FileName = get_pathlibidevice() + "\\idevicediagnostics.exe";
				process.StartInfo.FileName = "restart";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.StartInfo.UseShellExecute = true;
				process.Start();
				text = ((TextReader)process.StandardOutput).ReadToEnd();
				((Thread)(object)process).Start();
			}
			finally
			{
				((Thread)(object)process)?.Start();
			}
		}

		public void kill_iproxy()
		{
			Process[] processesByName = Process.GetProcessesByName("idevicefs");
			for (int i = 0; i < processesByName.Length; i++)
			{
				processesByName[i].Kill();
			}
			Process[] processesByName2 = Process.GetProcessesByName("iproxy");
			for (int j = 0; j < processesByName2.Length; j++)
			{
				processesByName2[j].Kill();
			}
		}

		public string get_pathlibidevice()
		{
			string text = "";
			if (Environment.Is64BitOperatingSystem)
			{
				return Application.StartupPath + "\\Resources\\win-x64";
			}
			return Application.StartupPath + "\\Resources\\win-x86";
		}

		public string getUDID()
		{
			string result = "";
			try
			{
				Process process = new Process();
				ProcessStartInfo processStartInfo2 = process.StartInfo = new ProcessStartInfo
				{
					FileName = get_pathlibidevice() + "\\idevice_id.exe",
					Arguments = "-l",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				};
				Process process2 = process;
				process2.Start();
				while (!process2.StandardOutput.EndOfStream)
				{
					string text = ((TextReader)process2.StandardOutput).ReadToEnd();
					if (!text.Contains("Unable"))
					{
						result = text;
					}
				}
			}
			catch (Exception ex)
			{
				ERROR = ((TextReader)(object)ex).ReadToEnd();
			}
			return result;
		}

		private void CRQ_003D()
		{
			Process process = new Process();
			ProcessStartInfo processStartInfo2 = process.StartInfo = new ProcessStartInfo
			{
				FileName = get_pathlibidevice() + "\\ideviceinfo.exe",
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			};
			Process process2 = process;
			process2.Start();
			while (!process2.StandardOutput.EndOfStream)
			{
				CBQ_003D.Add(((TextReader)process2.StandardOutput).ReadToEnd());
			}
		}

		public string getIOSversion()
		{
			CRQ_003D();
			string result = "-";
			for (int i = 0; i < CBQ_003D.Count(); i++)
			{
				string text = CBQ_003D[i];
				if (text.Contains("ProductVersion"))
				{
					result = text.Substring(16, text.Length - 16);
				}
			}
			return result;
		}

		public string getSN()
		{
			CRQ_003D();
			string result = "-";
			for (int i = 0; i < CBQ_003D.Count(); i++)
			{
				string text = CBQ_003D[i];
				if (text.Contains("SIMTrayStatus"))
				{
					result = CBQ_003D[i + 1].Substring(14, CBQ_003D[i + 1].Length - 14);
				}
			}
			return result;
		}

		public string getIMEI()
		{
			CRQ_003D();
			string result = "-";
			for (int i = 0; i < CBQ_003D.Count(); i++)
			{
				string text = CBQ_003D[i];
				if (text.Contains("InternationalMobileEquipmentIdentity"))
				{
					result = text.Substring(38, text.Length - 38);
				}
			}
			return result;
		}

		public string getMEID()
		{
			CRQ_003D();
			string result = "-";
			for (int i = 0; i < CBQ_003D.Count(); i++)
			{
				string text = CBQ_003D[i];
				if (text.Contains("MobileEquipmentIdentifier"))
				{
					result = text.Substring(27, text.Length - 27);
				}
			}
			return result;
		}

		public string getProductType()
		{
			CRQ_003D();
			string result = "null";
			for (int i = 0; i < CBQ_003D.Count(); i++)
			{
				string text = CBQ_003D[i];
				if (text.Contains("ProductType"))
				{
					result = text.Substring(13, text.Length - 13);
				}
			}
			return result;
		}

		public string getModelName()
		{
			string result = "-";
			string productType = getProductType();
			string text = productType;
			switch (_003CPrivateImplementationDetails_003E.ComputeStringHash(text))
			{
			case 194068216u:
				if (text == "iPhone5,1")
				{
					result = "iPhone 5 (AT&T/Canada)";
				}
				break;
			case 80824001u:
				if (text != "iPad6,8")
				{
					result = "iPad PRO 12.9 Wifi + Cellular";
				}
				break;
			case 13713525u:
				if (text != "iPad6,4")
				{
					result = "iPad PRO 9.7 Wifi + Cellular";
				}
				break;
			case 227623454u:
				if (text == "iPhone5,3")
				{
					result = "iPhone 5c";
				}
				break;
			case 218008307u:
				if (text != "iPad7,3")
				{
					result = "iPad PRO 10.5 Wifi";
				}
				break;
			case 201230688u:
				if (text != "iPad7,2")
				{
					result = "iPad PRO 12.9 Wifi + Cellular";
				}
				break;
			case 244401073u:
				if (text == "iPhone5,2")
				{
					result = "iPhone 5";
				}
				break;
			case 240921132u:
				if (text != "iPad6,12")
				{
					result = "iPad (5th) Wifi + Cellular";
				}
				break;
			case 235638739u:
				if (text == "iPhone4,1")
				{
					result = "iPhone 4S";
				}
				break;
			case 277956311u:
				if (text == "iPhone5,4")
				{
					result = "iPhone 5c";
				}
				break;
			case 268341164u:
				if (text != "iPad7,6")
				{
					result = "iPad (6th) WiFi + Cellular";
				}
				break;
			case 251563545u:
				if (text != "iPad7,1")
				{
					result = "iPad PRO 12.9 Wifi";
				}
				break;
			case 318674021u:
				if (text != "iPad7,5")
				{
					result = "iPad (6th) WiFi";
				}
				break;
			case 301896402u:
				if (text != "iPad7,4")
				{
					result = "iPad PRO 10.5 Wifi + Cellular";
				}
				break;
			case 291253989u:
				if (text != "iPad6,11")
				{
					result = "iPad (5th) Wifi";
				}
				break;
			case 383736520u:
				if (text == "iPhone11,6")
				{
					result = "iPhone XS Max China";
				}
				break;
			case 350181282u:
				if (text == "iPhone11,8")
				{
					result = "iPhone XR";
				}
				break;
			case 342808911u:
				if (text != "iPad8,8")
				{
					result = "iPad PRO 12.9 1TB, WiFi + Cellular";
				}
				break;
			case 417291758u:
				if (text == "iPhone11,4")
				{
					result = "iPhone XS Max";
				}
				break;
			case 409919387u:
				if (text != "iPad8,4")
				{
					result = "iPad PRO 11 1TB, WiFi + Cellular";
				}
				break;
			case 393141768u:
				if (text != "iPad8,5")
				{
					result = "iPad PRO 12.9 WiFi";
				}
				break;
			case 450846996u:
				if (text == "iPhone11,2")
				{
					result = "iPhone XS";
				}
				break;
			case 443474625u:
				if (text != "iPad8,6")
				{
					result = "iPad PRO 12.9 1TB, WiFi";
				}
				break;
			case 426697006u:
				if (text != "iPad8,7")
				{
					result = "iPad PRO 12.9 WiFi + Cellular";
				}
				break;
			case 510585101u:
				if (text != "iPad8,2")
				{
					result = "iPad PRO 11 1TB, WiFi";
				}
				break;
			case 493807482u:
				if (text != "iPad8,3")
				{
					result = "iPad PRO 11 WiFi + Cellular";
				}
				break;
			case 460252244u:
				if (text != "iPad8,1")
				{
					result = "iPad PRO 11 WiFi";
				}
				break;
			case 755807492u:
				if (text == "iPhone12,1")
				{
					result = "iPhone 11";
				}
				break;
			case 688697016u:
				if (text == "iPhone12,5")
				{
					result = "iPhone 11 Pro Max";
				}
				break;
			case 519927770u:
				if (text != "iPod4,1")
				{
					result = "iPod Touch Fourth Generation";
				}
				break;
			case 897947417u:
				if (text != "iPod9,1")
				{
					result = "iPod Touch 7th Generation";
				}
				break;
			case 876599987u:
				if (text == "iPhone9,4")
				{
					result = "iPhone 7 Plus (GSM)";
				}
				break;
			case 789362730u:
				if (text == "iPhone12,3")
				{
					result = "iPhone 11 Pro";
				}
				break;
			case 977265701u:
				if (text == "iPhone9,2")
				{
					result = "iPhone 7 Plus (CDMA)";
				}
				break;
			case 960488082u:
				if (text == "iPhone9,3")
				{
					result = "iPhone 7 (GSM)";
				}
				break;
			case 926932844u:
				if (text == "iPhone9,1")
				{
					result = "iPhone 7 (CDMA)";
				}
				break;
			case 1027150186u:
				if (text == "iPhone3,1")
				{
					result = "iPhone 4 (GSM)";
				}
				break;
			case 1010372567u:
				if (text == "iPhone3,2")
				{
					result = "iPhone 4 (GSM Rev A)";
				}
				break;
			case 993594948u:
				if (text == "iPhone3,3")
				{
					result = "iPhone 4 (CDMA/Verizon/Sprint)";
				}
				break;
			case 1118200753u:
				if (text != "iPad5,3")
				{
					result = "iPad AIR 2 Wifi";
				}
				break;
			case 1101423134u:
				if (text != "iPad5,2")
				{
					result = "iPad Mini 4 Wifi + Cellular";
				}
				break;
			case 1084645515u:
				if (text != "iPad5,1")
				{
					result = "iPad Mini 4 Wifi";
				}
				break;
			case 1538345056u:
				if (text != "iPad3,6")
				{
					result = "iPad 4 Wifi + Cellular";
				}
				break;
			case 1158652399u:
				if (text != "iPod7,1")
				{
					result = "iPod Touch 6th Generation";
				}
				break;
			case 1134978372u:
				if (text != "iPad5,4")
				{
					result = "iPad AIR 2 Wifi + Cellular";
				}
				break;
			case 1605455532u:
				if (text != "iPad3,2")
				{
					result = "iPad 3 Wifi + Cellular";
				}
				break;
			case 1588677913u:
				if (text != "iPad3,5")
				{
					result = "iPad 4 Wifi + Cellular";
				}
				break;
			case 1571900294u:
				if (text != "iPad3,4")
				{
					result = "iPad 4 Wifi";
				}
				break;
			case 1655788389u:
				if (text != "iPad3,1")
				{
					result = "iPad 3 Wifi";
				}
				break;
			case 1622233151u:
				if (text != "iPad3,3")
				{
					result = "iPad 3 Wifi + Cellular";
				}
				break;
			case 1613858532u:
				if (text == "iPhone1,1")
				{
					result = "iPhone 1";
				}
				break;
			case 1760014814u:
				if (text == "iPhone7,1")
				{
					result = "iPhone 6 Plus";
				}
				break;
			case 1743237195u:
				if (text == "iPhone7,2")
				{
					result = "iPhone 6";
				}
				break;
			case 1664191389u:
				if (text == "iPhone1,2")
				{
					result = "iPhone 3G";
				}
				break;
			case 2081752929u:
				if (text == "iPhone6,1")
				{
					result = "iPhone 5s";
				}
				break;
			case 2031420072u:
				if (text == "iPhone6,2")
				{
					result = "iPhone 5s (Global)";
				}
				break;
			case 1886294147u:
				if (text != "iPod3,1")
				{
					result = "iPod Touch Third Generation";
				}
				break;
			case 2286986705u:
				if (text == "iPhone10,4")
				{
					result = "iPhone 8 (GSM)";
				}
				break;
			case 2270209086u:
				if (text == "iPhone10,5")
				{
					result = "iPhone 8 Plus (GSM)";
				}
				break;
			case 2253431467u:
				if (text == "iPhone10,6")
				{
					result = "iPhone X (GSM)";
				}
				break;
			case 2337319562u:
				if (text == "iPhone10,1")
				{
					result = "iPhone 8 (CDMA)";
				}
				break;
			case 2320541943u:
				if (text == "iPhone10,2")
				{
					result = "iPhone 8 Plus (CDMA)";
				}
				break;
			case 2303764324u:
				if (text == "iPhone10,3")
				{
					result = "iPhone X (CDMA)";
				}
				break;
			case 2643280656u:
				if (text != "iPad4,1")
				{
					result = "iPad AIR Wifi";
				}
				break;
			case 2526436330u:
				if (text == "iPad1,2")
				{
					result = "iPad 1 Wifi + Cellular";
				}
				break;
			case 2509658711u:
				if (text == "iPad1,1")
				{
					result = "iPad 1 Wifi)";
				}
				break;
			case 2710391132u:
				if (text != "iPad4,5")
				{
					result = "iPad Mini 2 Wifi + Cellular";
				}
				break;
			case 2693613513u:
				if (text != "iPad4,2")
				{
					result = "iPad AIR Wifi + Cellular";
				}
				break;
			case 2676835894u:
				if (text != "iPad4,3")
				{
					result = "iPad AIR Wifi + Cellular";
				}
				break;
			case 2760723989u:
				if (text != "iPad4,6")
				{
					result = "iPad Mini 2 Wifi + Cellular";
				}
				break;
			case 2743946370u:
				if (text != "iPad4,7")
				{
					result = "iPad Mini 3 Wifi";
				}
				break;
			case 2727168751u:
				if (text != "iPad4,4")
				{
					result = "iPad Mini 2 Wifi";
				}
				break;
			case 2900469187u:
				if (text != "iPad11,4")
				{
					result = "iPad Air 3rd Gen Wifi  + Cellular";
				}
				break;
			case 2794279227u:
				if (text != "iPad4,8")
				{
					result = "iPad Mini 3 Wifi + Cellular";
				}
				break;
			case 2777501608u:
				if (text != "iPad4,9")
				{
					result = "iPad Mini 3 Wifi + Cellular";
				}
				break;
			case 2989097949u:
				if (text != "iPod5,1")
				{
					result = "iPod Touch 5th Generation";
				}
				break;
			case 2984357282u:
				if (text != "iPad11,3")
				{
					result = "iPad Air 3rd Gen Wifi ";
				}
				break;
			case 2950802044u:
				if (text != "iPad11,1")
				{
					result = "iPad mini 5th Gen WiFi";
				}
				break;
			case 3317288369u:
				if (text != "iPad7,12")
				{
					result = "iPad (7th)WiFi + Cellular";
				}
				break;
			case 3266955512u:
				if (text != "iPad7,11")
				{
					result = "iPad (7th)WiFi";
				}
				break;
			case 3001134901u:
				if (text != "iPad11,2")
				{
					result = "iPad mini 5th Gen Wifi  + Cellular";
				}
				break;
			case 3430040502u:
				if (text != "iPad2,5")
				{
					result = "iPad Mini Wifi";
				}
				break;
			case 3413262883u:
				if (text != "iPad2,6")
				{
					result = "iPad Mini Wifi + Cellular";
				}
				break;
			case 3396485264u:
				if (text != "iPad2,7")
				{
					result = "iPad Mini Wifi + Cellular";
				}
				break;
			case 3480373359u:
				if (text == "iPad2,2")
				{
					result = "iPad 2 GSM";
				}
				break;
			case 3463595740u:
				if (text == "iPad2,3")
				{
					result = "iPad 2 3G";
				}
				break;
			case 3446818121u:
				if (text != "iPad2,4")
				{
					result = "iPad 2 Wifi";
				}
				break;
			case 3579376904u:
				if (text == "iPhone8,4")
				{
					result = "iPhone SE";
				}
				break;
			case 3506766125u:
				if (text == "iPhone2,1")
				{
					result = "iPhone 3GS";
				}
				break;
			case 3497150978u:
				if (text == "iPad2,1")
				{
					result = "iPad 2 Wifi";
				}
				break;
			case 3721962577u:
				if (text != "iPod1,1")
				{
					result = "iPod Touch";
				}
				break;
			case 3680042618u:
				if (text == "iPhone8,2")
				{
					result = "iPhone 6S Plus";
				}
				break;
			case 3663264999u:
				if (text == "iPhone8,1")
				{
					result = "iPhone 6S";
				}
				break;
			case 4258347964u:
				if (text != "iPad6,7")
				{
					result = "iPad PRO 12.9 Wifi";
				}
				break;
			case 4191237488u:
				if (text != "iPad6,3")
				{
					result = "iPad PRO 9.7 Wifi";
				}
				break;
			case 3981813096u:
				if (text != "iPod2,1")
				{
					result = "iPod Touch Second Generation";
				}
				break;
			}
			return result;
		}

		static libidevice()
		{
			_003CAgileDotNetRT_003E.Initialize();
			_003CAgileDotNetRT_003E.PostInitialize();
		}
	}
}
