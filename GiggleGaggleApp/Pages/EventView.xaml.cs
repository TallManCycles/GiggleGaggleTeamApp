using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GiggleGaggleApp
{
	public partial class EventView : ContentPage
	{
		Event _e;
		Geocoder geoCoder;

		public EventView(Event e)
		{
			_e = e;
			geoCoder = new Geocoder();

			InitializeComponent();

			ToolbarItems.Add(new ToolbarItem
			{
				Text = "Edit",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(() => Button_Clicked(null, null))
			});
		}

		protected override async void OnAppearing()
		{
			DetailLayout.BindingContext = _e;

			var address = _e.MeetingLocation;
			var approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
			var position = approximateLocations.FirstOrDefault();

			var pin = new Pin
			{
				Type = PinType.Place,
				Position = position,
				Label = "Meeting Point",
				Address = _e.MeetingLocation
			};
			MeetupLocationMap.Pins.Add(pin);

			MeetupLocationMap.MoveToRegion(
			MapSpan.FromCenterAndRadius(
					position, Distance.FromKilometers(1)));



			AttendingButton.Text = "I'm Attending";


			if (_e.Image == null)
			{
				BikesquatButton.IsVisible = true;
				BikeSquatImage.Source = ImageSource.FromFile("KangarooP.JPG");
				BikeSquatImage.WidthRequest = App.ScreenSize.Width;
			}
			else
			{
				//BikesquatButton.IsVisible = false;
				BikeSquatImage.Source = _e.Image.ImageSource;
			}

			foreach (Event.rideType r in Enum.GetValues(typeof(Event.rideType)))
			{
				if (_e.RideType == r)
				{
					//RideTypeLabel.Text = r.ToString();
				}
			}

			foreach (Event.elevation e in Enum.GetValues(typeof(Event.elevation)))
			{
				if (_e.Elevation == e)
				{
					//ElevationTypeLabel.Text = e.ToString();
				}
			}

			base.OnAppearing();
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

		private async void AttendingClick(object sender, EventArgs e)
		{
			await DisplayAlert("confirmation", "You're attending!", "OK");
		}

		private async void Button_Clicked(object p1, object p2)
		{
			await this.Navigation.PushAsync(new EventDetailEdit(_e));
		}
	}
}

