using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;

namespace GiggleGaggleApp.Droid
{
	[Activity (Label = "Giggle Gaggle", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			CrashManager.Register(this, "e4a1f908bf294318a617d37642aec6d4");

			var size = new Xamarin.Forms.Size();
			var metrics = Resources.DisplayMetrics;
			size.Height = metrics.HeightPixels;
			size.Width = metrics.WidthPixels;

			App.Scale = metrics.Density;

			LoadApplication (new App ());
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			//CameraService.OnResult(resultCode);
		}
	}
}

