using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class Events : ContentPage
	{
		public Events()
		{
			InitializeComponent();

			ToolbarItems.Add(new ToolbarItem
			{
				Text = "Add",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(() => Navigation.PushAsync(new EventPageDetail(null)))
			});

			MainList.ItemsSource = GetDummyList();
		}

		public void OnItemTapped(Object sender, ItemTappedEventArgs e)
		{
			var eventSelected = e.Item as Event;
			if (eventSelected != null)
			{
				Navigation.PushAsync(new EventPageDetail(eventSelected));
			}

			MainList.SelectedItem = null;
		}

		public static IEnumerable<Event> GetDummyList()
		{
			List<Event> events = new List<Event>();


			for (int i = 0; i < 9; i++)
			{
				Event e = new Event()
				{
					Title = string.Format("Riverloop {0}", i),
					Description = "This is a bike squat event",
					Date = DateTime.Now.AddDays(i),
					forecast = new Forecast()
					{
						Main = "Rain",
						Description = "Light Rain",
						Degrees = 16
					},
				};

				events.Add(e);
			}

			return events;
		}
	}
}

