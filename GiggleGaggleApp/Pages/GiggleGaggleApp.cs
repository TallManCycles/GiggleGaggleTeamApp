using System;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public class App : Application, ILoginManager
	{
		public static App _Current;

		public App()
		{
			//SettingsService.IsLoggedIn = true;

			// we remember if they're logged in, and only display the login page if they're not
			if (SettingsService.IsLoggedIn)
				MainPage = new NavigationPage(new MasterPage(this));
			else
				MainPage = new LoginModalPage(this);
		}

		static volatile App _Instance;

		static object _SyncRoot = new Object();

		public static App Instance
		{
			get
			{
				if (_Instance == null)
				{
					lock (_SyncRoot)
					{
						if (_Instance == null)
						{
							_Instance = new App();
							_Instance.OAuthSettings =
								new OAuthSettings(
									clientId: "155908621479312",
									scope: "public_profile",
									authorizeUrl: "https://m.facebook.com/dialog/oauth/",
									redirectUrl: "http://www.facebook.com/connect/blank");
						}
					}
				}

				return _Instance;
			}
		}

		public OAuthSettings OAuthSettings { get; private set; }

		static string _Token;

		public static User User
		{
			get;
			set;
		}

		public static string Token
		{
			get
			{
				return _Token;
			}
		}

		public static void SaveToken(string token)
		{
			_Token = token;
		}

		public static void SaveUser(User user)
		{
			User = user;
		}

		public static Action SuccessfulLoginAction
		{
			get
			{
				var a = (App)App.Current;
				var action = new Action(a.ShowMainPage);
				action.Invoke();
				return action;
			}
		}

		public void ShowMainPage()
		{
			MainPage = new NavigationPage(new MasterPage(this));
			SettingsService.IsLoggedIn = true;
		}

		public void Logout()
		{
			SettingsService.IsLoggedIn = false;
			MainPage = new LoginModalPage(this);
		}

		public void ShowFacebookLogin()
		{
			MainPage = new NavigationPage(new FacebookLoginPage());
		}


		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

