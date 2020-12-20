using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using LogoLib;
using Microsoft.Win32;

namespace Wpf
{
	public partial class App : System.Windows.Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			this._components = new Container();
			this._notifyIcon = new NotifyIcon(this._components)
			{
				ContextMenuStrip = new ContextMenuStrip(),
				Icon = Wpf.Properties.Resources.tray,
				Text = "P870TM Cover LED Tray Tool",
				Visible = true
			};

			
			// !!!! IMPORTANT activate new instance of library
			this._logoManager = new LogoManager();
			this._utility = new Utility();
			this._osd = new OsdForm();

			
			//Check if silent, autostart and KB LED manage/enabled in saved settings
			silentSwitch = _utility.GetSilent();
			autostartSwitch = _utility.GetAutostart();
			manageKBswitch = _utility.GetManageKB();
			kbLEDSwitch = _utility.GetLastKBLEDstatus();
			
			this.AddMenuItems(this._notifyIcon.ContextMenuStrip.Items);
			this._notifyIcon.ContextMenuStrip.Opening += this.ContextMenuStrip_Opening;
			
			this._logoManager.OnLogoSet += delegate(int f, string s)
			{
					if (silentSwitch == false) {
					string color= _logoManager.GetLogoString(f);
					this._notifyIcon.ShowBalloonTip(1000, "Cover LED Tray Tool", string.Format("Cover LED color  set to '{0}'", color), ToolTipIcon.Info);

				}
			};
			this._logoManager.OnError += delegate(int f, string s)
			{
				if (silentSwitch == false) {
					this._notifyIcon.ShowBalloonTip(1000, "Cover LED Tray Tool", s, ToolTipIcon.Error);			
				}
			};
			
			//after start - set last saved settings for kb/logo
			this.RestoreLastLogo();
			if(manageKBswitch == true)	this.RestoreLastKBsettings();

			
			SystemEvents.PowerModeChanged += OnPowerChange;

		}

		
		//read and set last logo
		private void RestoreLastLogo()
		{
			int lastlogo = this._utility.GetLastLogo;
			_logoManager.SetLogoWmi(lastlogo);

			}
		
				//restore kb state
		private void RestoreLastKBsettings()
		{
			if (kbLEDSwitch == true) {
				_logoManager.SetLEDKBOn();

				
				_logoManager.SetKbBrightness(_utility.GetLastKBBacklight, kbLEDSwitch);
				_logoManager.SetKBTimeout(_utility.GetLastKBtimeout, kbLEDSwitch);
				int[] arrayLMR = _utility.GetLastKBleft;
				_logoManager.KBSetColorByPart(arrayLMR[0],arrayLMR[1],arrayLMR[2],"left");
				arrayLMR = _utility.GetLastKBmid;
				_logoManager.KBSetColorByPart(arrayLMR[0],arrayLMR[1],arrayLMR[2],"mid");
				arrayLMR = _utility.GetLastKBmid;
				_logoManager.KBSetColorByPart(arrayLMR[0],arrayLMR[1],arrayLMR[2],"right");
					
				
			}
			else {
				_logoManager.SetLEDKBOff();

			}

		}
		
		
		
		

		protected override void OnExit(ExitEventArgs e)
		{
			if (this._notifyIcon != null)
			{
				this._notifyIcon.Dispose();
			}
			if (this._components != null)
			{
				this._components.Dispose();
			}
			base.OnExit(e);
		}

		
		//manual setup of menu items
		private void AddMenuItems(ToolStripItemCollection items)
		{
			this._currentLogoLabel = new ToolStripLabel("???");
			items.Add(this._currentLogoLabel);
			
			items.Add(new ToolStripSeparator());
						
						
			this._menuBlack = new ToolStripMenuItem ("Set BLACK (Disable)", null, new EventHandler(this.setLogo_ClickByColor));
			_menuBlack.Tag = 0;
			items.Add(_menuBlack);
			
			this._menuOrange = new ToolStripMenuItem ("Orange", null, new EventHandler(this.setLogo_ClickByColor));
			_menuOrange.Tag = 1;
			items.Add(_menuOrange);
			
			this._menuBlue = new ToolStripMenuItem ("Blue", null, new EventHandler(this.setLogo_ClickByColor));
			_menuBlue.Tag = 2;
			items.Add(_menuBlue);
			
			this._menuWhiteBlue = new ToolStripMenuItem ("White Blue", null, new EventHandler(this.setLogo_ClickByColor));
			_menuWhiteBlue.Tag = 3;
			items.Add(_menuWhiteBlue);
			
			this._menuGreen = new ToolStripMenuItem ("Green", null, new EventHandler(this.setLogo_ClickByColor));
			_menuGreen.Tag = 4;
			items.Add(_menuGreen);
			
			this._menuYellow = new ToolStripMenuItem ("Yellow", null, new EventHandler(this.setLogo_ClickByColor));
			_menuYellow.Tag = 5;
			items.Add(_menuYellow);
			
			this._menuRed = new ToolStripMenuItem ("Red", null, new EventHandler(this.setLogo_ClickByColor));
			_menuRed.Tag = 6;
			items.Add(_menuRed);
			
			this._menuPurple = new ToolStripMenuItem ("Purple", null, new EventHandler(this.setLogo_ClickByColor));
			_menuPurple.Tag = 7;
			items.Add(_menuPurple);
						
			items.Add(new ToolStripSeparator());
			
			items.Add(new ToolStripMenuItem("Set KB", null, new EventHandler(this.setKB_Click)));
			
			items.Add(new ToolStripSeparator());
			
			this._menuManageKB = new ToolStripMenuItem ("Enable KB LED management", null, new EventHandler(this.EnableKBManage_Click));
			if (manageKBswitch==true) _menuManageKB.Checked=true;	
			items.Add(_menuManageKB);
			
			this._menuSilent = new ToolStripMenuItem ("Silent mode", null, new EventHandler(this.setSilent));
			if (silentSwitch==true) _menuSilent.Checked=true;
			items.Add(_menuSilent);

			
			this._menuAutostart = new ToolStripMenuItem ("Start with Windows", null, new EventHandler(this.setAutostart));
			if (autostartSwitch==true) _menuAutostart.Checked=true;
			items.Add(_menuAutostart);

			
			items.Add(new ToolStripSeparator());
			items.Add(new ToolStripMenuItem("Exit", null, new EventHandler(this.onExit_Clicked)));
		}

		
		//update current logo color in menu
		private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = false;
			int colorcode = this._utility.GetLastLogo;
			this._currentLogoLabel.Text = string.Format("Current: {0}", this._logoManager.GetLogoString(colorcode));


		}

		
		private void onExit_Clicked(object sender, EventArgs e)
		{
			base.Shutdown();
		}

		
		
				//show set kb windows
		private void setKB_Click(object sender, EventArgs e)
		{
			if (manageKBswitch==true){
			if (this._kbWindow == null || !this._kbWindow.IsLoaded)
			{
				this._kbWindow = new KBWindow(this._logoManager, this._utility, this._osd);
				this._kbWindow.Closing += delegate(object s, CancelEventArgs args)
				{
					this._kbWindow = null;
				};
			}
			this._kbWindow.Show();
		}
			else {
				this._notifyIcon.ShowBalloonTip(1000, "KB Manager ", string.Format("Enable KB Management setting "),ToolTipIcon.Error);
			}
		}
		
		//handler for clicking menu items by color
		private void setLogo_ClickByColor(object sender, EventArgs e)
		{
			ToolStripMenuItem clickeditem = (ToolStripMenuItem)sender;
			int logocolorcode = (int)clickeditem.Tag;
			bool result = _logoManager.SetLogoWmi(logocolorcode);
			if (result == true)_utility.SaveLastLogo(logocolorcode);

		}
		
				//handler for kbmanager 
		private void EnableKBManage_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem clickeditem = (ToolStripMenuItem)sender;
			if (clickeditem.Checked ==true) {
							clickeditem.Checked = false;
							_utility.SetManageKB (false);
							manageKBswitch = false;
			}
			else {
				clickeditem.Checked = true;
				_utility.SetManageKB (true);
				manageKBswitch = true;
				this.RestoreLastKBsettings();
			}

		}
		
		
		//handler enabling silent mode in menu
		private void setSilent(object sender, EventArgs e)
		{
			ToolStripMenuItem clickeditem = (ToolStripMenuItem)sender;
			
			// disable silent mode
			if (clickeditem.Checked ==true) {
							clickeditem.Checked = false;
							_utility.SetSilent (false);
							silentSwitch = false;
			}
			//enable silent mode
			else {
				clickeditem.Checked = true;
				_utility.SetSilent (true);
				silentSwitch = true;
			}

		}
		
		//handler autostart routine
		private void setAutostart(object sender, EventArgs e)
		{
			ToolStripMenuItem clickeditem = (ToolStripMenuItem)sender;
			
			
			if (clickeditem.Checked ==true) {
							clickeditem.Checked = false;
								autostartSwitch = false;
							_utility.SetAutostart (autostartSwitch);

			}
			//enable autostart
			else {
				clickeditem.Checked = true;
				autostartSwitch = true;
				_utility.SetAutostart(autostartSwitch);


				}
		
			}
		
		
		//restore cover LED after sleep mode
		private void OnPowerChange(object s, PowerModeChangedEventArgs e){
			
			switch (e.Mode){
				case PowerModes.Resume:
					this.RestoreLastLogo();
					if (manageKBswitch == true) this.RestoreLastKBsettings();
					break;
					
				case PowerModes.Suspend:
					
					break;
						
			}
		}

		private Container _components;

		private NotifyIcon _notifyIcon;

		private LogoManager _logoManager;
		
		private Utility _utility;

		private KBWindow _kbWindow;

		private ToolStripLabel _currentLogoLabel;
		
		private ToolStripMenuItem _menuBlack; 
							private ToolStripMenuItem _menuOrange;
							private ToolStripMenuItem _menuBlue;
							private ToolStripMenuItem _menuWhiteBlue;
							private ToolStripMenuItem _menuGreen;
							private ToolStripMenuItem _menuYellow;
							private ToolStripMenuItem _menuRed;
							private ToolStripMenuItem _menuPurple;
		private ToolStripMenuItem _menuSilent;
		private ToolStripMenuItem _menuAutostart;
		private ToolStripMenuItem _menuManageKB;
				
		private bool silentSwitch;
		private bool autostartSwitch;
		private bool kbLEDSwitch;
		private bool manageKBswitch;
		
		private OsdForm _osd;
	}
	

}
