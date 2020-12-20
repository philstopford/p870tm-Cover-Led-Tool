using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using LogoLib;

namespace Wpf
{
	/// <summary>
	/// Interaction logic for KBWiindow.xaml
	/// </summary>
	public partial class KBWindow : Window
	{
		public KBWindow(LogoManager logo, Utility utility, OsdForm osd)
		{
			InitializeComponent();
			this._logo = logo;
			this._utility = utility;
			this._osd = osd;
			SetInterface();


		}
		
		void SetInterface() {
			
			int[] lastKBleft = _utility.GetLastKBleft;
			boxRedL.Text = lastKBleft [0].ToString();
			boxGreenL.Text = lastKBleft [1].ToString();
			boxBlueL.Text = lastKBleft [2].ToString();
			
						int[] lastKBmid = _utility.GetLastKBmid;
			boxRedM.Text = lastKBmid [0].ToString();
			boxGreenM.Text = lastKBmid [1].ToString();
			boxBlueM.Text = lastKBmid [2].ToString();
			
						int[] lastKBright = _utility.GetLastKBright;
			boxRedR.Text = lastKBright [0].ToString();
			boxGreenR.Text = lastKBright [1].ToString();
			boxBlueR.Text = lastKBright [2].ToString();
			
			ComboBoxItem backlightLevel0 = new ComboBoxItem();
			ComboBoxItem backlightLevel1 = new ComboBoxItem();
			ComboBoxItem backlightLevel2 = new ComboBoxItem();
			ComboBoxItem backlightLevel3 = new ComboBoxItem();
			ComboBoxItem backlightLevel4 = new ComboBoxItem();
			backlightLevel0.Content = "KB LED Backlight off";
			backlightLevel1.Content = "KB LED Backlight 25%";
			backlightLevel2.Content = "KB LED Backlight 50%";
			backlightLevel3.Content = "KB LED Backlight 75%";	
			backlightLevel4.Content = "KB LED Backlight 100%";
			
			
			backlightSetBox.Items.Add(backlightLevel0);
			backlightSetBox.Items.Add(backlightLevel1);
			backlightSetBox.Items.Add(backlightLevel2);
			backlightSetBox.Items.Add(backlightLevel3);
						backlightSetBox.Items.Add(backlightLevel4);
			
			ComboBoxItem backlightTimeout0 = new ComboBoxItem();
			ComboBoxItem backlightTimeout1 = new ComboBoxItem();
			ComboBoxItem backlightTimeout2 = new ComboBoxItem();
			ComboBoxItem backlightTimeout3 = new ComboBoxItem();
			ComboBoxItem backlightTimeout4 = new ComboBoxItem();
			backlightTimeout0.Content = "KB LED timeout OFF";
			backlightTimeout1.Content = "KB LED timeout 15s";
			backlightTimeout2.Content = "KB LED timeout 30s";
			backlightTimeout3.Content = "KB LED timeout 3m";	
			backlightTimeout4.Content = "KB LED timeout 15m";
			
			
			backlightTimeoutBox.Items.Add(backlightTimeout0);
			backlightTimeoutBox.Items.Add(backlightTimeout1);
			backlightTimeoutBox.Items.Add(backlightTimeout2);
			backlightTimeoutBox.Items.Add(backlightTimeout3);
			backlightTimeoutBox.Items.Add(backlightTimeout4);
			
			
			ComboBoxItem logoColor0 = new ComboBoxItem();
			ComboBoxItem logoColor1 = new ComboBoxItem();
			ComboBoxItem logoColor2 = new ComboBoxItem();
			ComboBoxItem logoColor3 = new ComboBoxItem();
			ComboBoxItem logoColor4 = new ComboBoxItem();
			ComboBoxItem logoColor5 = new ComboBoxItem();
			ComboBoxItem logoColor6 = new ComboBoxItem();
			ComboBoxItem logoColor7 = new ComboBoxItem();
			logoColor0.Content = "Cover: Black (disabled)";
			logoColor1.Content = "Cover: Orange";
			logoColor2.Content = "Cover: Blue";
			logoColor3.Content = "Cover: White Blue";	
			logoColor4.Content = "Cover: Green";
			logoColor5.Content = "Cover: Yellow";
			logoColor6.Content = "Cover: Red";
			logoColor7.Content = "Cover: Purple";
			backlightLogoBox.Items.Add(logoColor0);
			backlightLogoBox.Items.Add(logoColor1);
			backlightLogoBox.Items.Add(logoColor2);
			backlightLogoBox.Items.Add(logoColor3);
			backlightLogoBox.Items.Add(logoColor4);
			backlightLogoBox.Items.Add(logoColor5);
			backlightLogoBox.Items.Add(logoColor6);
			backlightLogoBox.Items.Add(logoColor7);
			
			backlightSetBox.SelectedIndex = _utility.GetLastKBBacklight;
			backlightTimeoutBox.SelectedIndex = _utility.GetLastKBtimeout;
			backlightLogoBox.SelectedIndex =_utility.GetLastLogo;
		}

		
		
		
		void setLeftKB_Click(object sender, RoutedEventArgs e)
		{


			redL = int.Parse(boxRedL.Text);
			greenL = int.Parse(boxGreenL.Text);
			blueL = int.Parse(boxBlueL.Text);
			_logo.KBSetColorByPart(redL, greenL,blueL,"left");
			_utility.SaveKBleft(redL, greenL, blueL);
			
		}

		
		void setMidKB_Click(object sender, RoutedEventArgs e)
		{
			redM = int.Parse(boxRedM.Text);
			greenM = int.Parse(boxGreenM.Text);
			blueM = int.Parse(boxBlueM.Text);
			_logo.KBSetColorByPart(redM, greenM,blueM,"mid");
			_utility.SaveKBmid(redM, greenM, blueM);
			
		}
		
		void setRightKB_Click(object sender, RoutedEventArgs e)
		{
						redR = int.Parse(boxRedR.Text);
						greenR = int.Parse(boxGreenR.Text);
						blueR = int.Parse(boxBlueR.Text);
						_logo.KBSetColorByPart(redR, greenR,blueR,"right");
						_utility.SaveKBright(redR, greenR, blueR);
			
		}
		
				void setAllKB_Click(object sender, RoutedEventArgs e)
		{
						redM = int.Parse(boxRedM.Text);
						greenM = int.Parse(boxGreenM.Text);
						blueM = int.Parse(boxBlueM.Text);
						_logo.KBSetColorByPart(redM, greenM,blueM,"left");
						_logo.KBSetColorByPart(redM, greenM,blueM,"mid");
						_logo.KBSetColorByPart(redM, greenM,blueM,"right");
						_utility.SaveKBleft(redM, greenM, blueM);
						_utility.SaveKBmid(redM, greenM, blueM);
						_utility.SaveKBright(redM, greenM, blueM);
						this.boxRedL.Text = redM.ToString();
						this.boxGreenL.Text = greenM.ToString();
						this.boxBlueL.Text = blueM.ToString();
						
							this.boxRedR.Text = redM.ToString();
						this.boxGreenR.Text = greenM.ToString();
						this.boxBlueR.Text = blueM.ToString();
			
		}
		
		
		
				void backlightSetBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				ComboBox clickeditem1 = (ComboBox)sender;
				int index = clickeditem1.SelectedIndex;
				if (index == _utility.GetLastKBBacklight) {

				}
				else {
					
				_logo.SetKbBrightness(index, _utility.GetLastKBLEDstatus());
				_utility.SaveLastKBBacklight(index);
				
				_osd.ShowKBBrightness(index*25);
					if (index ==0) 
					{
						_utility.SaveLastKBLEDstatus(false);
					}
					else {
						_utility.SaveLastKBLEDstatus(true);
					}
				}

				
		}
				
						
				void backlightTimeoutBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

				ComboBox clickeditem2 = (ComboBox)sender;
				int index = clickeditem2.SelectedIndex;
				if (index ==_utility.GetLastKBtimeout) {

				}
				else {
					_logo.SetKBTimeout(index, _utility.GetLastKBLEDstatus());
					_utility.SaveLastKBtimeout(index);	
					_osd.ShowKBTimeout(index, time[index]);
				}


				
		}
				
								void backlightLogoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				ComboBox clickeditem3 = (ComboBox)sender;
				int index = clickeditem3.SelectedIndex;
				
				if (index ==_utility.GetLastLogo) {

				}
				else {
					if (index==0) {

					if (_utility.GetLastKBLEDstatus()==true) {
						_logo.SetLogoWmi(index);
					}
					else {
						_logo.SetLogoWmi(index);
						_logo.SetLEDKBOff();
					}

				}

				else{
						if (_utility.GetLastKBLEDstatus()==true) 
						{
							_logo.SetLogoWmi(index);
						}
						else {
							_logo.SetLogoWmi(index);
							_logo.SetLEDKBOff();
						}
				}
				
				_utility.SaveLastLogo(index);
				}
				
				


				
		}
				
		private int redL;
		private int greenL;
		private int blueL;
		private int redM;
		private int greenM;
		private int blueM;
		private int redR;
		private int greenR;
		private int blueR;
		private LogoManager _logo;
		private Utility _utility;
		private OsdForm _osd;
		string[] time = {"off", "15s", "30s", "3m", "15m"};

		

		
		

		

		

		

		
		//events for more usefull gui

		
		void boxRedL_GotMouseCapture(object sender, MouseEventArgs e)
		{
			boxRedL.SelectAll();				
		}
		void boxRedM_GotMouseCapture(object sender, MouseEventArgs e)
		{
			boxRedM.SelectAll();				
		}
				void boxRedR_GotMouseCapture(object sender, MouseEventArgs e)
		{
			boxRedR.SelectAll();				
		}
						void boxBlueL_GotMouseCapture(object sender, MouseEventArgs e)
		{
			boxBlueL.SelectAll();				
		}
								void boxBlueM_GotMouseCapture(object sender, MouseEventArgs e)
		{
			boxBlueM.SelectAll();				
		}
								
								void boxBlueR_GotMouseCapture(object sender, MouseEventArgs e)
		{
			boxBlueR.SelectAll();				
		}
								
								
								void boxGreenL_GotMouseCapture(object sender, MouseEventArgs e)
		{
			boxGreenL.SelectAll();				
		}
								void boxGreenM_GotMouseCapture(object sender, MouseEventArgs e)
		{
			boxGreenM.SelectAll();				
		}
								void boxGreenR_GotMouseCapture(object sender, MouseEventArgs e)
		{
			boxGreenR.SelectAll();				
		}
		
		

		
		void backlightTimeoutBox_DropDownClosed(object sender, EventArgs e)
		{
			ComboBox clickeditem2 = (ComboBox)sender;
				int index = clickeditem2.SelectedIndex;
				_osd.ShowKBTimeout(index, time[index]);

		}
		
		void backlightSetBox_DropDownClosed(object sender, EventArgs e)
		{
				ComboBox clickeditem2 = (ComboBox)sender;
				int index = clickeditem2.SelectedIndex;
				_osd.ShowKBBrightness(index*25);
		}
	}
}