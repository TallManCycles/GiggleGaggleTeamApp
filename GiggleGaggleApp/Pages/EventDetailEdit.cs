using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class EventDetailEdit : ContentPage
	{
		Event _e;
		bool isNew;

		public EventDetailEdit(Event e)
		{
			InitializeComponent();

			if (e != null)
				_e = e;
			else
				isNew = true;
				_e = new Event();
		}

		protected override void OnAppearing()
		{
			DetailLayout.BindingContext = _e;

			if (_e.Image == null)
			{
				BikesquatButton.IsVisible = true;
				BikeSquatImage.Source = ImageSource.FromFile("KangarooP.JPG");
			}
			else
			{
				BikesquatButton.IsVisible = false;
				BikeSquatImage.Source = _e.Image.ImageSource;
			}

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

			if (result)
			{
				await Navigation.PopAsync();
			}
		}

		private async void OnCancel(Object sender, EventArgs e)
		{
			var result = await DisplayAlert("Confirmation", "Are you sure you want to cancel?", "Yes", "No");

			if (result)
				await Navigation.PopAsync();
		}

		private async void TakePicture(Object sender, EventArgs e)
		{
			var pictureResult = await DependencyService.Get<ICammeraService>().TakePictureAsync();

			EventPhoto image = _e.Image;

			if (pictureResult != null)
			{
				if (image == null)
				{
					image = new EventPhoto();
					image.DateTaken = DateTime.Now;
					image.ImageSource = pictureResult.Image;
					image.Title = _e.Title;
					_e.Image = image;
				}
				else
				{
					var result = await DisplayAlert("Image Override", "Would you like to overwrite the current event image?", "Yes", "No");

					if (result)
					{
						image.DateTaken = DateTime.Now;
						image.ImageSource = pictureResult.Image;
						image.Title = _e.Title;
						_e.Image = image;
					}
				}

				OnPropertyChanged();
			}
		}
	}
}

