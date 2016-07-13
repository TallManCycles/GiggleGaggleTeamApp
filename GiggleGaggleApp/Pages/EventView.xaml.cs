using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class EventView : ContentPage
	{
		Event _e;

		public EventView(Event e)
		{
			_e = e;

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
			DetailLayout.BindingContext = _e;

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

		private async void Button_Clicked(object p1, object p2)
		{
			await this.Navigation.PushAsync(new EventDetailEdit(_e));
		}
	}
}

