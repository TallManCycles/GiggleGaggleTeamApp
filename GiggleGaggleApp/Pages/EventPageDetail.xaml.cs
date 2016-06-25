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

			if (isNew)
			{
				EventDate.Date = DateTime.Now.AddDays(1);
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

