using System;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.Win32;

namespace LogoLib
{
	public class Talk2Wmi
	{

		
		
				
				public void InitHotkey()
		{
					
					
										
					CheckMofReg();
					

			if (this.classInstance == null)
			{
				this.classInstance = new ManagementObject("root\\WMI", "CLEVO_GET.InstanceName='ACPI\\PNP0C14\\0_0'", null);
			}

						
			

		}


						private void CheckMofReg()
		{
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\WmiAcpi\\", true);
				string a = registryKey.GetValue("MofImagePath", "").ToString();
				if (a == "" || a != "syswow64\\clevomof.dll")
				{
					registryKey.SetValue("MofImagePath", "syswow64\\clevomof.dll", RegistryValueKind.String);
				}
				registryKey.Close();
			}
			catch
			{
			}
		}
				

		public bool InvokeMethod(string arg2, string data, string name)
		{
			bool result;
			try
			{
				uint num = Convert.ToUInt32(data);
				this.inParams = this.classInstance.GetMethodParameters(name);
				this.inParams["Data"] = num;
				this.outParams = this.classInstance.InvokeMethod(name, this.inParams, null);

				result = true;
			}
			catch (Exception ex)
			{

				result = false;
			}
			return result;
		}

	
	

		
		

		[DllImport("kernel32")]
		public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retStr, int bufferSize, string filePath);

		[DllImport("Kernel32.dll")]
		private static extern bool Wow64EnableWow64FsRedirection(bool Wow64FsEnableRedirection);



		private ManagementObject classInstance;


		private ManagementBaseObject outParams;

		private ManagementBaseObject inParams;




	}
}
