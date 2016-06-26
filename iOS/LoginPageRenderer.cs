using System;
using GiggleGaggleApp;
using GiggleGaggleApp.iOS;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace GiggleGaggleApp.iOS
{
	public class LoginPageRenderer : PageRenderer
	{
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			var auth = new OAuth2Authenticator(
				clientId: App.Instance.OAuthSettings.ClientId,
				scope: App.Instance.OAuthSettings.Scope,
				authorizeUrl: new Uri(App.Instance.OAuthSettings.AuthorizeUrl),
				redirectUrl: new Uri(App.Instance.OAuthSettings.RedirectUrl));

			auth.Completed += (sender, eventArgs) =>
			{
				// We presented the UI, so it's up to us to dimiss it on iOS.
				App.SuccessfulLoginAction.Invoke();

				if (eventArgs.IsAuthenticated)
				{
					// Use eventArgs.Account to do wonderful things
					App.SaveToken(eventArgs.Account.Properties["access_token"]);
				}
				else {
					// The user cancelled
				}
			};

			PresentViewController(auth.GetUI(), true, null);
		}
	}
}

