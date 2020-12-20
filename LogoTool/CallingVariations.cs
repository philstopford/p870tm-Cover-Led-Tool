using System;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace LogoLib
{
	public class CallingVariations
	{
		

				[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		
	}
}
