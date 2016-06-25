using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class BikeSquatCameraPage : ContentPage
	{
		readonly Image image = new Image();

		public static BikeSquatCameraPage Instance;

		public BikeSquatCameraPage()
		{
			Instance = this;

			InitializeComponent();

			var button = new Button
			{
				Text = "Snap!",
				Command = new Command(o => ShouldTakePicture()),
			};

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.Center,
				Children = {
					button,
					image,
					},
			};
		}

		public event Action ShouldTakePicture = () => { };

		public void ShowImage(string filepath)
		{
			image.Source = ImageSource.FromFile(filepath);
		}
}
}

