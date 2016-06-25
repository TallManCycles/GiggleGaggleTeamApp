using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class BikeSquatCameraPage : ContentPage
	{
		ICammeraService _cameraService;

		public BikeSquatCameraPage(ICammeraService cammeraService)
		{
			_cameraService = cammeraService;

			Title = "Bike Squats";

			ToolbarItems.Add(new ToolbarItem
			{
				Text = "+",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(() => Button_Clicked(null, null))
			});

			InitializeComponent();
		}

		public async void Button_Clicked(Object sender, EventArgs e)
		{
			var pictureResult = await _cameraService.TakePictureAsync();

			if (pictureResult != null)
			{
				EventPhoto photo = new EventPhoto();
				photo.image = new Image();
				photo.image.Source = pictureResult.Image;
				photo.DateTaken = DateTime.Now;

				PhotoGrid.BindingContext = photo;

				OnPropertyChanged();
			}
		}
	}
}

