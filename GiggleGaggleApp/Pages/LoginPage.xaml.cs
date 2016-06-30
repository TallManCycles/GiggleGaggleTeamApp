using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class LoginPage : ContentPage
	{
		ILoginManager _ilm;

		public LoginPage(ILoginManager ilm)
		{
			_ilm = ilm;

			InitializeComponent();
		}

		private void Login(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
			{
				DisplayAlert("Error", "Username and Password are required", "Re-try");
			}
			else {
				App.Current.Properties["IsLoggedIn"] = true;
				_ilm.ShowMainPage();
			}
		}

		private void Register(object sender, EventArgs e)
		{
			MessagingCenter.Send<ContentPage>(this, "Create");
		}
	}
}

