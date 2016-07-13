using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class WeeklyRidePage : ContentPage
	{
		Event _event;

		public WeeklyRidePage()
		{
			InitializeComponent();

			ToolbarItems.Add(new ToolbarItem
			{
				Text = "Edit",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(() => Button_Clicked(null, null))
			});
		}

		protected override void OnAppearing()
		{
			_event = Events.GetDummyList().FirstOrDefault();

			this.BindingContext = _event;

			List<string> rideTypeList = new List<string>();

			foreach (Event.rideType r in Enum.GetValues(typeof(Event.rideType)))
			{
				rideTypeList.Add(r.ToString());
				RideTypePicker.Items.Add(r.ToString());
			}

			RideTypePicker.SelectedIndex = rideTypeList.IndexOf(_event.RideType.ToString());

			List<string> elevationType = new List<string>();

			foreach (Event.elevation e in Enum.GetValues(typeof(Event.elevation)))
			{
				elevationType.Add(e.ToString());
				ElevationPicker.Items.Add(e.ToString());
			}

			ElevationPicker.BindingContext = elevationType;
			ElevationPicker.SelectedIndex = elevationType.IndexOf(_event.Elevation.ToString());

			base.OnAppearing();
		}

		public async void Button_Clicked(Object sender, EventArgs e)
		{
			await this.Navigation.PushAsync(new EventDetailEdit(_event));
		}
	}
}

