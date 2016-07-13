using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using WeatherNet;
using WeatherNet.Model;

namespace GiggleGaggleApp
{
	public partial class MasterPage : ContentPage
	{
		ILoginManager _loginManager;

		WeatherService _weatherService;


		public MasterPage (ILoginManager loginManager)
		{
			InitializeComponent ();

			_weatherService = new WeatherService();

			_loginManager = loginManager;

			NavigationPage.SetHasNavigationBar(this, false);

			Event eventDisplay = Events.GetDummyList().FirstOrDefault();

			if (eventDisplay.Date < DateTime.Now.AddDays(16))
			{
				var weatherResult = _weatherService.GetSixteenDayForcast();

				if (!weatherResult.Success)
				{
					eventDisplay.Temperature = 0.0;
					eventDisplay.WeatherDescription = "No weather results found";
				}
				else
				{
					var item = weatherResult.Items.FirstOrDefault(x => UnixTimeStampToDateTime(x.DateUnixFormat).Date == eventDisplay.Date.Date);
					if (item != null)
					{
						eventDisplay.Temperature = item.Temp;
						eventDisplay.WeatherDescription = item.Description;
					}
					else
					{
						eventDisplay.Temperature = 0.0;
						eventDisplay.WeatherDescription = "Too far in the future to tell";
					}
				}
			}

			DisplayEvent.BindingContext = eventDisplay;
				
			MainList.ItemsSource = GetMenuItems();

			var tap = new TapGestureRecognizer();

			tap.Tapped += DisplayEventTapped;

			DisplayEvent.GestureRecognizers.Add(tap);
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

		private async void DisplayEventTapped(object sender, EventArgs e)
		{
			if (sender != null)
			{
				var ev = (Event)this.DisplayEvent.BindingContext;
				await Navigation.PushAsync(new EventView(ev));
			}
		}

		public List<MenuItem> GetMenuItems()
		{
			List<MenuItem> menu = new List<MenuItem>();

			//MenuItem routes = new MenuItem();
			//routes.MenuTitle = "Suggested Routes";
			//routes.TargetPage = new SuggestedRoutesPage();
			////routes.IconImage = new FileImageSource() { File = "eventicon.png" };
			//menu.Add(routes);

			MenuItem upcomingEvents = new MenuItem();
			upcomingEvents.MenuTitle = "Upcoming Events";
			upcomingEvents.TargetPage = new Events(Events.EventType.Future);
			//events.IconImage = new FileImageSource() { File = "eventicon.png" };
			menu.Add(upcomingEvents);

			MenuItem pastEvents = new MenuItem();
			pastEvents.MenuTitle = "Past Events";
			pastEvents.TargetPage = new Events(Events.EventType.Past);
			//events.IconImage = new FileImageSource() { File = "eventicon.png" };
			menu.Add(pastEvents);

			//MenuItem cammera = new MenuItem();
			//cammera.MenuTitle = "Bikesquats";
			//cammera.TargetPage = new BikeSquatCameraPage(DependencyService.Get<ICammeraService>());
			//menu.Add (cammera);

			MenuItem userDetails = new MenuItem();
			userDetails.MenuTitle = "Logout";
			menu.Add(userDetails);

			return menu;
		}

		public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
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

