using System;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public class LoginModalPage : CarouselPage
	{
		ContentPage login, create, facebookLogin;

		public LoginModalPage(ILoginManager ilm)
		{
			login = new LoginPage(ilm);
			create = new CreateAccountPage(ilm);
			facebookLogin = new FacebookLoginPage();

			this.Children.Add(login);
			this.Children.Add(create);
			this.Children.Add(facebookLogin);

			MessagingCenter.Subscribe<ContentPage>(this, "Login", (sender) =>
			{
				this.SelectedItem = login;
			});
			MessagingCenter.Subscribe<ContentPage>(this, "Create", (sender) =>
			{
				this.SelectedItem = create;
			});
			MessagingCenter.Subscribe<ContentPage>(this, "FacebookLogin", (sender) =>
			{
				this.SelectedItem = login;
			});
		}
	}
}


