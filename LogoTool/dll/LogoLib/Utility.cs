/*
 * Created by SharpDevelop.
 * User: secondofficer
 * Date: 16/12/2020
 * Time: 12:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace LogoLib

{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Utility
	{
		public Utility()
		{
		}
		
		
		public bool GetAutostart()
		{
			

			
			if ( this._settings.Autostart == true) {
					//FOOLPROOF?? check if autostart correctly enabled in registry for exe 
					//get path
					_execPath = System.Reflection.Assembly.GetEntryAssembly().Location;
						try{
	
						RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
						string value = rk.GetValue("Cover LED Tray Tool").ToString();
		
								if (_execPath==value) {

									return	this._settings.Autostart;
												
								}
								else {
									System.Diagnostics.Debug.WriteLine("path not the same, rewriting");
									rk.DeleteValue("Cover LED Tray Tool",false);
									rk.SetValue("Cover LED Tray Tool",_execPath);
									return	this._settings.Autostart;
								}
						
						} catch (Exception) {
							return	false;
						}
			
			}
			
			else {
				return	this._settings.Autostart;
			}
			
		}

		
		public bool SetAutostart(bool autostart)
		{
			this._settings.Autostart = autostart;
			_execPath = System.Reflection.Assembly.GetEntryAssembly().Location;
			
			// disable autostart
			if(autostart==false){
				try{
								
					RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
					rk.DeleteValue("Cover LED Tray Tool",false);
					return true;
					} catch (Exception) {
						return false;
					}
				}
			//enable autostart
			else {

				try{

					RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
					rk.SetValue("Cover LED Tray Tool",_execPath);
					return true;
					} catch (Exception) {
						return false;
					}
			}
			}
			
		public bool GetSilent()
		{
			return	this._settings.Silent;
		}

		public void SetSilent(bool silent)
		{
			this._settings.Silent = silent;
		}
		
		public bool GetManageKB()
		{
			return	this._settings.ManageKB;
		}

		public void SetManageKB (bool manage)
		{
			this._settings.ManageKB = manage;
		}
		
		
		public bool GetLastKBLEDstatus()
		{
			return	this._settings.KBenabledLED;
		}

		public void SaveLastKBLEDstatus(bool enabled)
		{
			this._settings.KBenabledLED = enabled;
		}
		

				
		public int GetLastLogo
		{
			get
			{
				return this._settings.LastLogo;
			}
		}
		
		public void SaveLastLogo (int logo)
		{

				this._settings.LastLogo = logo;
		}
		

		
		public void SaveKBleft (int r, int g, int b)
		{

				this._settings.LastKBredL = r;
				this._settings.LastKBgreenL = g;
				this._settings.LastKBblueL = b;
		}
		
		public void SaveKBmid (int r, int g, int b)
		{

				this._settings.LastKBredM = r;
				this._settings.LastKBgreenM = g;
				this._settings.LastKBblueM = b;
		}
				public void SaveKBright (int r, int g, int b)
		{

				this._settings.LastKBredR = r;
				this._settings.LastKBgreenR = g;
				this._settings.LastKBblueR = b;
		}
		
				public int[] GetLastKBleft
		{
			get
			{
				int[] rgbarray = {this._settings.LastKBredL, this._settings.LastKBgreenL, this._settings.LastKBblueL};
				return rgbarray;
			}
		}
		
				public int[] GetLastKBmid
		{
			get
			{
				int[] rgbarray = {this._settings.LastKBredM, this._settings.LastKBgreenM, this._settings.LastKBblueM};
				return rgbarray;
			}
		}
						
				public int[] GetLastKBright
		{
			get
			{
				int[] rgbarray = {this._settings.LastKBredR, this._settings.LastKBgreenR, this._settings.LastKBblueR};
				return rgbarray;
			}
		}
				
		//backlight
				public int GetLastKBBacklight
		{
			get
			{
				return this._settings.LastKBbacklight;
			}
		}
		
		public void SaveLastKBBacklight (int level)
		{
											this._settings.LastKBbacklight = level;
			if (level == 0) {
				this._settings.KBenabledLED = false;

			}
			else{
								this._settings.KBenabledLED = true;
			}
		}
		
		//timeout backlight
		
		
		public int GetLastKBtimeout
		{
			get
			{
				return this._settings.LastKBtimeout;
			}
		}
		
		public void SaveLastKBtimeout (int timeout)
		{

				this._settings.LastKBtimeout = timeout;
		}
		

		
		private SettingStore _settings = new SettingStore();
		
		private string _execPath;
}
}