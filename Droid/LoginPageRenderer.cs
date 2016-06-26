using System;
using Android.App;
using GiggleGaggleApp;
using OAuth2Demo.XForms.Android;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace OAuth2Demo.XForms.Android
{
	public class LoginPageRenderer : PageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			// this is a ViewGroup - so should be able to load an AXML file and FindView<>
			var activity = this.Context as Activity;

			var auth = new OAuth2Authenticator(
				clientId: App.Instance.OAuthSettings.ClientId,
				scope: App.Instance.OAuthSettings.Scope,
				authorizeUrl: new Uri(App.Instance.OAuthSettings.AuthorizeUrl),
				redirectUrl: new Uri(App.Instance.OAuthSettings.RedirectUrl));

			auth.Completed += (sender, eventArgs) =>
			{
				if (eventArgs.IsAuthenticated)
				{
					App.SuccessfulLoginAction.Invoke();
					// Use eventArgs.Account to do wonderful things
					App.SaveToken(eventArgs.Account.Properties["access_token"]);
				}
				else {
					// The user cancelled
				}
			};

			activity.StartActivity(auth.GetUI(activity));
		}
	}
}