using System;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace LogoLib
{
	public class LogoManager
	
	{
		public event LogoManager.LogoEventHandler OnLogoSet;

		public event LogoManager.LogoEventHandler OnError;
		
		public event LogoManager.KBEventHandler OnKBSet;

		public event LogoManager.KBEventHandler OnKBError;




		public LogoManager()
		{
			this.OnLogoSet += delegate(int f, string s)
			{
			};
			this.OnError += delegate(int f, string s)
			{
			};
			
			this.OnKBSet += delegate(int f, string s)
			{
			};
			this.OnKBError += delegate(int f, string s)
			{
			};
			
			this.wmi = new Talk2Wmi();
			this.wmi.InitHotkey();
		}


		//Settings KB LED RGB by each part		
		public void KBSetColorByPart(int _iR, int _iG, int _iB, string KbPart)
		{
			int num = (int)Math.Round((double)_iB * 200.0 / 255.0);
			long num2 = (long)((_iB << 16) + _iG + (_iR << 8));
			
			if (KbPart == "left")
			{
				num2 = (long)((num << 16) + _iG + (_iR << 8));
				num2 |= 4026531840u;
			}
			else if (KbPart == "mid")
			{
				num2 = (num << 16) + _iG + (_iR << 8);
				num2 |= 4043309056u;
			}
			else if (KbPart == "right")
			{
				num2 = (long)((num << 16) + _iG + (_iR << 8));
				num2 |= 4060086272u;
			}
			
			//cover switching
			else if (KbPart == "cover")
			{
				if (_iB == 0 && _iG == 147 && _iR == 255)
				{
					num2 = 51300L;
				}
				else
				{
					num2 = (long)((_iB / 2 << 16) + _iG / 2 + (_iR / 2 << 8));
				}
				this.SetLogoOnOff(0);
				this.wmi.InvokeMethod(wmiCodeKB103, 1073741824L.ToString(), wmiCommandKBLED);
				num2 |= 4127195136u;
				this.wmi.InvokeMethod(wmiCodeKB103, num2.ToString(), wmiCommandKBLED);
				this.wmi.InvokeMethod(wmiCodeKB103, 1073807360L.ToString(), wmiCommandKBLED);
				this.SetLogoOnOff(1);
				return;
			}
			
			this.wmi.InvokeMethod(wmiCodeKB103, num2.ToString(), wmiCommandKBLED);
			
		}
		
		
		public void SetLogoOnOff(int status)
		{
			
			switch (status) {
			case 0:
				this.wmi.InvokeMethod(wmiCodeKB103, "3758608391", wmiCommandKBLED);
			break;
			
			case 1:
				this.wmi.InvokeMethod(wmiCodeKB103, "3758616583", wmiCommandKBLED);
			break;
			}


		}
			
			public bool SetLogoWmi(int colorcode)
		{
							
							bool result = false;
							
																		
										//set logo off
										this.wmi.InvokeMethod(wmiCodeKB103, "3758608391", wmiCommandKBLED);
										this.wmi.InvokeMethod(wmiCodeKB103, "1073741824", wmiCommandKBLED);
										
										//set logo color
										result = this.wmi.InvokeMethod(wmiCodeKB103, wmicolorcodes[colorcode], wmiCommandKBLED);

										//set logo on
										this.wmi.InvokeMethod(wmiCodeKB103, "1073807360", wmiCommandKBLED);
										this.wmi.InvokeMethod(wmiCodeKB103, "3758616583", wmiCommandKBLED);	

							if (result==true)
							{
								this.OnLogoSet(colorcode, null);
								return result;
							}
							else
							{
								this.OnError(colorcode, string.Format("Failed to set Cover Led Color, are you using Clevo P870XX ? Check clevomof.dll to be in /syswow64/ folder."));
								return false;
							}

		}
		
		public string GetLogoString(int colorcode)
		{
			return colorstring [colorcode];
		}
		
			
		//enable LED KB with LOGO
		public void SetLEDKBOn()
		{
			this.wmi.InvokeMethod(wmiCodeKB103, "3758616577", wmiCommandKBLED);
		}
		//disable LED KB with LOGO
		public void SetLEDKBOff()
		{
			this.wmi.InvokeMethod(wmiCodeKB103, "3758096391", wmiCommandKBLED);
		}
		
		
		//setting KB+LOGO brightness
		public void SetKbBrightness(int index, bool kbLEDwasEnabled)
		{
			
			switch (index) {
		case 0:
			SetLEDKBOff();
			break;
		case 1:
			if (kbLEDwasEnabled ==false) SetLEDKBOn();
			this.wmi.InvokeMethod(wmiCodeKB103, wmibrightnesscodes[index], wmiCommandKBLED);
			break;
		case 2:
			if (kbLEDwasEnabled ==false) SetLEDKBOn();
			this.wmi.InvokeMethod(wmiCodeKB103,wmibrightnesscodes[index] , wmiCommandKBLED);
			break;
		case 3:
			if (kbLEDwasEnabled ==false) SetLEDKBOn();
			this.wmi.InvokeMethod(wmiCodeKB103, wmibrightnesscodes[index], wmiCommandKBLED);
			break;
		case 4:
			if (kbLEDwasEnabled ==false) SetLEDKBOn();
			this.wmi.InvokeMethod(wmiCodeKB103,wmibrightnesscodes[index] , wmiCommandKBLED);
			break;
			}
		}
		
		//setting KB only timeout
		public void SetKBTimeout(int index, bool kbLEDwasEnabled)
		{
			
			switch (index) {
		case 0:
			if (kbLEDwasEnabled ==false) return;
			this.wmi.InvokeMethod(wmiCodeKB121,wmitimeoutcodes[index] , wmiCommandKBtimeout);
			break;
		case 1:
			SetLEDKBOn();
			if (kbLEDwasEnabled ==false) return;
			this.wmi.InvokeMethod(wmiCodeKB121, wmitimeoutcodes[index], wmiCommandKBtimeout);
			break;
		case 2:
			if (kbLEDwasEnabled ==false) return;
			this.wmi.InvokeMethod(wmiCodeKB121, wmitimeoutcodes[index], wmiCommandKBtimeout);
			break;
		case 3:
			if (kbLEDwasEnabled ==false) return;
			this.wmi.InvokeMethod(wmiCodeKB121,wmitimeoutcodes[index] , wmiCommandKBtimeout);
			break;
		case 4:
			if (kbLEDwasEnabled ==false) return;
			this.wmi.InvokeMethod(wmiCodeKB121, wmitimeoutcodes[index], wmiCommandKBtimeout);
			break;
			}
		}

		
		private Talk2Wmi wmi;
		
		private const string wmiCodeKB103 = "103";
		private const string wmiCodeKB121 = "121";
		private const string wmiCommandKBLED = "SetKBLED";
		private const string wmiCommandKBtimeout = "SystemControlFunction";
		private string[] colorstring = {"Black (disabled)", "Orange", "Blue", "White Blue", "Green", "Yellow", "Red", "Purple"};
		private string[] wmicolorcodes = {"4127195136", "4127246436", "4135518208", "4135518335", "4127195263", "4127227775", "4127227648", "4135550720"};
		private string[] wmibrightnesscodes = {"no code disable kb", "4093640751", "4093640799", "4093640847","4093640895"};
		private string[] wmitimeoutcodes = {"402653184", "402653185", "402653186", "402653187", "402653188"};
		


		//events for logo and KB change
		public delegate void LogoEventHandler(int logo, string message);
		public delegate void KBEventHandler(int kb, string message);
	}
}
