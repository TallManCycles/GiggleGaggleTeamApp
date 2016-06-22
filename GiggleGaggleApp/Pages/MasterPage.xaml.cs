using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class MasterPage : ContentPage
	{
		public MasterPage ()
		{
			InitializeComponent ();

			DateLabel.Text = DateTime.Now.ToString("d");
		}

		protected override void OnAppearing()
		{
			List<MenuItems> menu = new List<MenuItems> ();

			MenuItems m = new MenuItems ();
			m.MenuTitle = "Bikesquat Camera";

			menu.Add (m);

			MainList.ItemsSource = menu;
		}
	}

	public class MenuItems
	{
		public MenuItems()
		{
			
		}

		public string MenuTitle {
			get;
			set;
		}
		public Type MenuType {
			get;
			set;
		}
	}
}

