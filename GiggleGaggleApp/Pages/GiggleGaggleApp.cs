using System;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public class App : Application
	{
		static NavigationPage _NavPage;

		public App()
		{
			MainPage = GetMainPage();
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


		public static Page GetMainPage()
		{
			var profilePage = new MasterPage();

			_NavPage = new NavigationPage(profilePage);

			return _NavPage;
		}

		public static bool IsLoggedIn
		{
			get
			{
				return !string.IsNullOrEmpty(_Token);
			}
		}

		static string _Token;

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

		public static Action SuccessfulLoginAction
		{
			get
			{
				return new Action(() =>
			   {
				   _NavPage.Navigation.PopAsync();
			   });
			}
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

