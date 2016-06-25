using System;
using System.Threading.Tasks;
using GiggleGaggleApp.iOS;
using GiggleGaggleApp;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(CameraService))]
namespace GiggleGaggleApp
{
	public class CameraService : ICammeraService
	{
		public CameraService()
		{
		}

		/// <summary>
		/// Takes the picture async.
		/// </summary>
		/// <returns>The picture async.</returns>
		public Task<CameraResult> TakePictureAsync()
		{
			var tcs = new TaskCompletionSource<CameraResult>();

			// Use the camera helper to launch the picker     
			Camera.TakePicture(
				UIApplication.SharedApplication.KeyWindow.RootViewController,
				(imagePickerResult) =>
				{
					//??
				});

			return tcs.Task;
		}
	}
}


