using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Facebook.CoreKit;
using Foundation;
using UIKit;
using HockeyApp.iOS;

namespace GiggleGaggleApp.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication(new App());

			Profile.EnableUpdatesOnAccessTokenChange(true);
			Settings.AppID = App.Instance.OAuthSettings.ClientId;
			Settings.DisplayName = "Giggle Gaggle";

			var manager = BITHockeyManager.SharedHockeyManager;
			manager.Configure("616fb4fca5c94837a3df3f2781c0db09");
			manager.StartManager();

			Xamarin.Forms.Size size = new Xamarin.Forms.Size();
			size.Height = UIScreen.MainScreen.Bounds.Height;
			size.Width = UIScreen.MainScreen.Bounds.Width;

			App.ScreenSize = size;

			App.Scale = UIScreen.MainScreen.Scale;

			Xamarin.FormsMaps.Init();

			return base.FinishedLaunching(app, options);
		}

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			App.SuccessfulLoginAction.Invoke();

			// We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
			return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
		}
	}
}

