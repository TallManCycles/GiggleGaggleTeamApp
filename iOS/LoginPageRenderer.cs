using System;
using System.Collections.Generic;
using CoreGraphics;
using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using GiggleGaggleApp;
using GiggleGaggleApp.iOS;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace GiggleGaggleApp.iOS
{
	public class LoginPageRenderer : PageRenderer
	{
		LoginButton loginButton;

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			List<string> readPermissions = new List<string> { "public_profile", "email" };

			Profile.Notifications.ObserveDidChange((sender, e) =>
			{

				if (e.NewProfile == null)
					return;

				//nameLabel.Text = e.NewProfile.Name;
			});

			loginButton = new LoginButton(new CGRect(80, 20, 220, 46))
			{
				LoginBehavior = LoginBehavior.SystemAccount,
				ReadPermissions = readPermissions.ToArray()
			};

			loginButton.TouchUpInside += (sender, e) =>
			{
				
			};

			loginButton.Completed += (sender, e) =>
			{
				App.SaveToken(AccessToken.CurrentAccessToken.TokenString);
				App.SuccessfulLoginAction.Invoke();

				if (e.Error != null)
				{
					// Handle if there was an error
					new UIAlertView("Login", e.Error.Description, null, "Ok", null).Show();
					return;
				}

				if (e.Result.IsCancelled)
				{
					// Handle if the user cancelled the login request
					new UIAlertView("Login", "The user cancelled the login", null, "Ok", null).Show();
					return;
				}


			};

			// Handle actions once the user is logged out
			loginButton.LoggedOut += (sender, e) =>
			{
				// Handle your logout
				new UIAlertView("Login", "Logout", null, "Ok", null).Show();
			};

			// If you have been logged into the app before, ask for the your profile name
			if (AccessToken.CurrentAccessToken != null)
			{
				var request = new GraphRequest("/me?fields=name", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
				request.Start((connection, result, error) =>
				{
					// Handle if something went wrong with the request
					if (error != null)
					{
						new UIAlertView("Error...", error.Description, null, "Ok", null).Show();
						return;
					}

					// Get your profile name
					var userInfo = result as NSDictionary;

					//App.SaveToken(userInfo["token"].ToString());
					//nameLabel.Text = userInfo["name"].ToString();
				});
			}

			// Add views to main view
			View.AddSubview(loginButton);
			//View.AddSubview(pictureView);
			//View.AddSubview(nameLabel);

			//var auth = new OAuth2Authenticator(
			//	clientId: App.Instance.OAuthSettings.ClientId,
			//	scope: App.Instance.OAuthSettings.Scope,
			//	authorizeUrl: new Uri(App.Instance.OAuthSettings.AuthorizeUrl),
			//	redirectUrl: new Uri(App.Instance.OAuthSettings.RedirectUrl));

			//auth.Completed += (sender, eventArgs) =>
			//{
			//	// We presented the UI, so it's up to us to dimiss it on iOS.
			//	App.SuccessfulLoginAction.Invoke();

			//	if (eventArgs.IsAuthenticated)
			//	{
			//		// Use eventArgs.Account to do wonderful things
			//		App.SaveToken(eventArgs.Account.Properties["access_token"]);
			//	}
			//	else {
			//		// The user cancelled
			//	}
			//};

			//PresentViewController(this, true, null);
		}
	}
}

