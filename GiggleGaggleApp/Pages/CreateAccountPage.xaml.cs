using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class CreateAccountPage : ContentPage
	{
		ILoginManager _ilm;

		public CreateAccountPage(ILoginManager ilm)
		{
			_ilm = ilm;
			InitializeComponent();
		}

		private async void Create(object sender, EventArgs e)
		{
			if (string.CompareOrdinal(Password.Text, RePassword.Text) != 0)
			{
				await DisplayAlert("Error", "Your passwords do not match.", "Ok");
				return;
			}

			// Do create login stuff here

			_ilm.ShowMainPage();
		}

		private async void Cancel(object sender, EventArgs e)
		{
			var result = await DisplayAlert("Confirmation", "Are you sure you want to cancel?", "No", "Yes");

			if (result)
			{
				MessagingCenter.Send<ContentPage>(this, "Login");
			}
		}

		private void FacebookRegister(object sender, EventArgs e)
		{
			_ilm.ShowFacebookLogin();
		}
	}
}

