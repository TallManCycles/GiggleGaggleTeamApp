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

			NavigationPage.SetHasNavigationBar(this, false);

			DateLabel.Text = DateTime.Now.ToString();
		}

		protected override void OnAppearing()
		{
			MainList.ItemsSource = GetMenuItems();
		}

		private async void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as MenuItem;
			if (item != null)
			{
				await Navigation.PushAsync(item.TargetPage);
			}
		}

		public static List<MenuItem> GetMenuItems()
		{
			List<MenuItem> menu = new List<MenuItem>();

			MenuItem m1 = new MenuItem();
			m1.MenuTitle = "Bikesquat Camera";
			m1.TargetPage = new BikeSquatCameraPage();
			m1.IconImage = new FileImageSource() { File = "camera.png" };
			menu.Add (m1);

			MenuItem m2 = new MenuItem();
			m2.MenuTitle = "Upcoming Events";
			m2.TargetPage = new Events();
			m2.IconImage = new FileImageSource() { File = "eventicon.png" };
			menu.Add(m2);

			return menu;
		}
	}

	public class MenuItem
	{
		public MenuItem()
		{
			
		}

		public string MenuTitle {
			get;
			set;
		}
		public ContentPage TargetPage {
			get;
			set;
		}

		public ImageSource IconImage
		{
			get;
			set;
		}
	}
}

