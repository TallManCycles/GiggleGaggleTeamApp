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

			InitializeComponent();

			Title = "Bike Squats";

			ToolbarItems.Add(new ToolbarItem
			{
				Text = "+",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(() => Button_Clicked(null, null))
			});

			MainList.BindingContext = DummyImages();
		}

		public async void Button_Clicked(Object sender, EventArgs e)
		{
			var pictureResult = await _cameraService.TakePictureAsync();

			if (pictureResult != null)
			{
				EventPhoto photo = new EventPhoto();
				photo.ImageSource = pictureResult.Image;
				photo.DateTaken = DateTime.Now;

				MainList.BindingContext = photo;

				OnPropertyChanged();
			}
		}

		public void OnItemTapped(Object sender, EventArgs e)
		{
			
		}

		public IEnumerable<EventPhoto> DummyImages()
		{
			List<EventPhoto> images = new List<EventPhoto>();

			EventPhoto p = new EventPhoto();
			p.ImageSource = "Nudgee.jpg";
			p.DateTaken = DateTime.Now;
			p.Title = "Demo";

			images.Add(p);

				//new Image() { Source = "Nudgee.jpg" },
				//new Image() { Source = "Nudgee2.JPG" },
				//new Image() { Source = "Nudgee3.jpg" },

			return images;
		}
	}
}

