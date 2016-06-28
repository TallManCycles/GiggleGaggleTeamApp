using System;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public class BaseContentPage : ContentPage
	{
		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!App.IsLoggedIn)
			{
				Navigation.PushAsync(new LoginPage());
			}
		}
	}
}


