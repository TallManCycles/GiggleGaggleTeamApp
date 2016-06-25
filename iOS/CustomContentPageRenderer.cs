using System;
using System.IO;
using Foundation;
using GiggleGaggleApp;
using GiggleGaggleApp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomContentPageRenderer))]

namespace GiggleGaggleApp.iOS
{
	public class CustomContentPageRenderer : PageRenderer
	{
		public CustomContentPageRenderer()
		{
		}

		public override public override void ApplicationFinishedRestoringState()
		{
			base.ApplicationFinishedRestoringState();
		}

		public override void FinishedLaunching(bool animated)
		{
			try
			{

				var imagePicker = new UIImagePickerController { SourceType = UIImagePickerControllerSourceType.Camera };

				BikeSquatCameraPage.Instance.ShouldTakePicture += () => PresentViewController(imagePicker, true, null);

				imagePicker.FinishedPickingMedia += (sender, e) =>
				{
					var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "tmp.png");
					var image = (UIImage)e.Info.ObjectForKey(new NSString("UIImagePickerControllerOriginalImage"));
					InvokeOnMainThread(() =>
					{
						image.AsPNG().Save(filepath, false);
						BikeSquatCameraPage.Instance.ShowImage(filepath);
					});
					DismissViewController(true, null);
				};

				imagePicker.Canceled += (sender, e) => DismissViewController(true, null);

				base.ViewDidAppear(animated);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

