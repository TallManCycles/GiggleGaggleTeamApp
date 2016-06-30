using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class MasterPage : ContentPage
	{
		ILoginManager _loginManager;

		public MasterPage (ILoginManager loginManager)
		{
			InitializeComponent ();

			_loginManager = loginManager;

			NavigationPage.SetHasNavigationBar(this, false);

			ApiService ws = new ApiService();

			ws.GetForecast();

			DisplayEvent.BindingContext = Events.GetDummyList().FirstOrDefault();

			MainList.ItemsSource = GetMenuItems();
		}

		private async void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as MenuItem;
			if (item != null && item.TargetPage != null)
			{
				await Navigation.PushAsync(item.TargetPage);
			}
			else
			{
				_loginManager.Logout();
			}

			MainList.SelectedItem = null;
		}

		public List<MenuItem> GetMenuItems()
		{
			List<MenuItem> menu = new List<MenuItem>();

			MenuItem weekly = new MenuItem();
			weekly.MenuTitle = "Weekly Ride";
			weekly.TargetPage = new WeeklyRidePage();
			//weekly.IconImage = 
			menu.Add(weekly);

			MenuItem routes = new MenuItem();
			routes.MenuTitle = "Suggested Routes";
			routes.TargetPage = new SuggestedRoutesPage();
			//routes.IconImage = new FileImageSource() { File = "eventicon.png" };
			menu.Add(routes);

			MenuItem events = new MenuItem();
			events.MenuTitle = "Upcoming Events";
			events.TargetPage = new Events();
			//events.IconImage = new FileImageSource() { File = "eventicon.png" };
			menu.Add(events);

			MenuItem cammera = new MenuItem();
			cammera.MenuTitle = "Bikesquats";
			cammera.TargetPage = new BikeSquatCameraPage(DependencyService.Get<ICammeraService>());
			//cammera.IconImage = new FileImageSource() { File = "camera.png" };
			menu.Add (cammera);

			MenuItem userDetails = new MenuItem();
			userDetails.MenuTitle = "Logout";
			//cammera.IconImage = new FileImageSource() { File = "camera.png" };
			menu.Add(userDetails);



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

