using System;
using GiggleGaggleApp.iOS;
using Xamarin.Social.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticationService))]
namespace GiggleGaggleApp.iOS
{
	public class AuthenticationService : IAuthenticationService
	{
		private FacebookService mFacebook;

		public AuthenticationService()
		{
			// 1. Create the service
			mFacebook = new FacebookService
			{
				ClientId = "155908621479312",
				RedirectUrl = new System.Uri("<Redirect URL from developers.facebook.com/apps>")
			};

		}

		public FacebookService AuthenticateUser()
		{
			Xamarin.Auth.Authenticator;


			if (mFacebook != null)
			{
				Xamarin.Auth.Authenticator auth = mFacebook.GetAuthenticateUI();
			}
		}

		public bool IsUserAuthenticated()
		{
					mFacebook.GetAccountsAsync().ContinueWith(accounts =>
					{
						// accounts is an IEnumerable<Account> of saved accounts
					});
		}
					                    

		public static FacebookService Facebook
		{
			get
			{
				if (mFacebook == null)
				{
					mFacebook = new FacebookService()
					{
						ClientId = "155908621479312",
						RedirectUrl = new Uri("Redirect URL from https://developers.facebook.com/apps")
					};
				}

				return mFacebook;
			}
		}
	}
}

