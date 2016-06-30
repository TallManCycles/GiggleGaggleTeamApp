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

[assembly: ExportRenderer(typeof(FacebookLoginPage), typeof(LoginPageRenderer))]
namespace GiggleGaggleApp.iOS
{
	public class LoginPageRenderer : PageRenderer
	{
		LoginButton loginButton;
		UILabel nameLabel;
		ProfilePictureView pictureView;

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			List<string> readPermissions = new List<string> { "public_profile", "email" };

			Profile.Notifications.ObserveDidChange((sender, e) =>
			{

				if (e.NewProfile == null)
					return;

				nameLabel.Text = e.NewProfile.Name;
				pictureView.PictureMode = ProfilePictureMode.Square;

				User u = new User();

				u.Name = e.NewProfile.FirstName;
				u.ImageUrl = e.NewProfile.ImageUrl(ProfilePictureMode.Square, new CGSize(70,70)).ToString();

				App.SaveUser(u);
			});

			loginButton = new LoginButton(new CGRect(80, 20, 220, 46))
			{
				LoginBehavior = LoginBehavior.Native,
				ReadPermissions = readPermissions.ToArray()
			};

			nameLabel = new UILabel();

			pictureView = new ProfilePictureView(new CGRect(80, 90, 220, 46));

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
			View.AddSubview(pictureView);
			View.AddSubview(nameLabel);
		}
	}
}

