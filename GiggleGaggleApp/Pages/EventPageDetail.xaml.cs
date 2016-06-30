using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class EventPageDetail : ContentPage
	{
		Event _e;
		bool isNew;

		public EventPageDetail(Event e)
		{
			InitializeComponent();

			if (e != null)
				_e = e;
			else
				isNew = true;
		}

		protected override void OnAppearing()
		{
			DetailLayout.BindingContext = _e;

			List<string> rideTypeList = new List<string>();

			foreach (Event.rideType r in Enum.GetValues(typeof(Event.rideType)))
			{
				rideTypeList.Add(r.ToString());
				RideTypePicker.Items.Add(r.ToString());
			}

			List<string> elevationType = new List<string>();

			foreach (Event.elevation e in Enum.GetValues(typeof(Event.elevation)))
			{
				elevationType.Add(e.ToString());
				ElevationPicker.Items.Add(e.ToString());
			}

			ElevationPicker.BindingContext = elevationType;


			if (isNew)
			{
				EventDate.Date = DateTime.Now.AddDays(1);
			}
			else
			{
				RideTypePicker.SelectedIndex = rideTypeList.IndexOf(_e.RideType.ToString());
				ElevationPicker.SelectedIndex = elevationType.IndexOf(_e.Elevation.ToString());
			}

			base.OnAppearing();
		}

		private async void OnSave(Object sender, EventArgs e)
		{
			var result = await DisplayAlert("Confirmation", "Are you sure you want to save this event?", "Yes", "No");

			//do something with result;
		}

		private async void OnCancel(Object sender, EventArgs e)
		{
			var result = await DisplayAlert("Confirmation", "Are you sure you want to cancel?", "Yes", "No");

			await Navigation.PopAsync();
		}
	}
}

